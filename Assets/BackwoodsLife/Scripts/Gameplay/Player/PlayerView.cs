using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Gameplay.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
    public class PlayerView : UIView
    {
        private IPlayerViewModel _viewModel;
        private readonly CompositeDisposable _disposables = new();
        private float _moveSpeed;
        private float _rotationSpeed;
        private Animator _animator;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private static readonly int MoveValue = Animator.StringToHash("MoveValue");
        private static readonly int IsMoving = Animator.StringToHash("Moving");
        private static readonly int IsGathering = Animator.StringToHash("Gathering");

        private bool _movementBlocked;
        private static readonly int IsInAction = Animator.StringToHash("IsInAction");

        [Inject]
        private void Construct(IPlayerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();

            Assert.IsNotNull(_animator, "Animator is null");

            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");
            Subscribe();
        }

        private void Subscribe()
        {
            _viewModel.MoveSpeed
                .Subscribe(moveSpeed => { _moveSpeed = moveSpeed; })
                .AddTo(_disposables);

            _viewModel.RotationSpeed
                .Subscribe(rotationSpeed => { _rotationSpeed = rotationSpeed; })
                .AddTo(_disposables);

            _viewModel.MoveDirection
                .Subscribe(newDirection =>
                {
                    if (_movementBlocked)
                    {
                        _moveDirection = Vector3.zero;
                        _animator.SetFloat(MoveValue, 0.0f);
                    }
                    else
                    {
                        _moveDirection = newDirection;
                        if (newDirection != Vector3.zero)
                        {
                            SetAnimatorBool("IsMoving", true);
                        }

                        _animator.SetFloat(MoveValue, newDirection.magnitude);
                    }
                })
                .AddTo(_disposables);

            _viewModel.CharacterAction
                .Subscribe(x => SetAnimatorBool(x, true))
                .AddTo(_disposables);

            _viewModel.CancelCharacterAction
                .Subscribe(x => SetAnimatorBool(x, false))
                .AddTo(_disposables);

            _viewModel.IsInAction
                .Subscribe(isInAction =>
                {
                    Debug.LogWarning($"<color=red>IsInAction = {isInAction}</color>");
                    _movementBlocked = isInAction;
                    _animator.SetBool(IsInAction, isInAction);
                })
                .AddTo(_disposables);
        }

        private void SetAnimatorBool(string s, bool value)
        {
            Debug.LogWarning($"<color=red>Anim {s} to {value}. IsInAction = {_movementBlocked}</color>");
            _animator.SetBool(Animator.StringToHash(s), value);
        }

        private void FixedUpdate() => CharacterMovement();

        private void CharacterMovement()
        {
            // Если персонаж выполянет действие, то не передвигаем и не вращаем
            if (_movementBlocked) return;

            MoveCharacter();
            RotateCharacter();
        }

        private void MoveCharacter()
        {
            if (_moveDirection == Vector3.zero) return;

            _rb.position += _moveDirection * (_moveSpeed * Time.fixedDeltaTime);
            _viewModel.SetModelPosition(_rb.position);
        }

        private void RotateCharacter()
        {
            if (_moveDirection.sqrMagnitude <= 0) return;

            var rotation = Quaternion.Lerp(
                _rb.rotation,
                Quaternion.LookRotation(_moveDirection, Vector3.up),
                Time.fixedDeltaTime * _rotationSpeed);

            _rb.rotation = rotation;
            _viewModel.SetModelRotation(rotation);
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
