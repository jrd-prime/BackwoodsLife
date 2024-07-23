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
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsGathering = Animator.StringToHash("IsGathering");

        private bool _isInAction;
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
                    if (_isInAction)
                    {
                        Debug.LogWarning("In action");
                        _animator.SetBool(IsMoving, false);
                        _moveDirection = Vector3.zero;
                    }
                    else
                    {
                        if (newDirection != Vector3.zero)
                        {
                            Debug.LogWarning("NOT In action and direct NOT 0");
                            _animator.SetBool(IsMoving, true);
                            _animator.SetFloat(MoveValue, newDirection.magnitude);
                            _moveDirection = newDirection;
                        }
                        else
                        {
                            Debug.LogWarning("NOT In action and direct 0");
                            _moveDirection = newDirection;
                            _animator.SetBool(IsMoving, false);
                            _animator.SetFloat(MoveValue, 0.0f);
                        }
                    }
                })
                .AddTo(_disposables);

            _viewModel.InAction
                .Skip(1)
                .Subscribe(InAction)
                .AddTo(_disposables);
        }

        private async void InAction(InActionData inActionData)
        {
            switch (inActionData.InteractType)
            {
                case EInteractType.Gathering:
                    Debug.LogWarning("Gathering action");
                    _animator.SetBool(IsGathering, true);
                    _animator.SetBool(IsInAction, true);
                    _isInAction = true;
                    await UniTask.Delay(6000);
                    _animator.SetBool(IsGathering, false);
                    _animator.SetBool(IsInAction, false);
                    _isInAction = false;
                    break;
                case EInteractType.Mining:
                    break;
                case EInteractType.Fishing:
                    break;
                case EInteractType.Hunting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero) MoveCharacter();
            if (_moveDirection.sqrMagnitude > 0) RotateCharacter();
        }

        private void MoveCharacter()
        {
            _rb.position += _moveDirection * (_moveSpeed * Time.fixedDeltaTime);
            _viewModel.SetModelPosition(_rb.position);
        }

        private void RotateCharacter()
        {
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
