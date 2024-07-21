using BackwoodsLife.Scripts.Gameplay.UI;
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
                    _moveDirection = newDirection;
                    _animator.SetFloat(MoveValue, newDirection.magnitude);
                })
                .AddTo(_disposables);
            //
            // _joy.MoveDirection
            //     .Subscribe(joystickDirection => { Debug.LogWarning("Move " + joystickDirection.magnitude); })
            //     .AddTo(_disposables);
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
