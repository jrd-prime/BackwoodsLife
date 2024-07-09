using System;
using BackwoodsLife.Scripts.Data.Player;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private IPlayerViewModel _viewModel;
        private readonly CompositeDisposable _disposables = new();

        // Components
        private Animator _animator;
        private Rigidbody _rigidbody;
        private Vector3 moveDirection;
        private Quaternion rotateDirection;

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();
            
            _viewModel.Rigidbody = _rigidbody;
            _viewModel.Animator =_animator;

            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            // _viewModel.PlayerPosition
            //     .Subscribe(PositionHandler)
            //     .AddTo(_disposables);

            _viewModel.MoveDirection
                .Subscribe(PositionHandler)
                .AddTo(_disposables);

            _viewModel.PlayerRotation
                .Subscribe(RotationHandler)
                .AddTo(_disposables);

            _viewModel.PlayAnimationByName
                .Skip(1)
                // .DistinctUntilChanged()
                .Subscribe(StartAnimation)
                .AddTo(_disposables);
        }

        private void PositionHandler(Vector3 position)
        {
            _rigidbody.position = position;
        }

        private void RotationHandler(Quaternion rotationQuaternion)
        {
            _rigidbody.rotation = rotationQuaternion;
        }

        private void FixedUpdate()
        {
            _rigidbody.position += moveDirection * (5f * Time.fixedDeltaTime);
            if (moveDirection.sqrMagnitude > 0)
            {
                // We create a Quaternion, the type of variable we use to represent rotations and
                // we use Quaternion.LookRotation to look at our moveInput vector which always points
                // towards the moving direction, and we say that we want to rotate the Vector3.up (Y axis).
                Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                // Then we pass that rotation to our Rigidbody rot using Quaternion.Lerp which is a method
                // to interpolate between two quaternions by a given time. In our case we use as the first
                // Quaternion the _Rigidbody.rotation and as a second Quaternion our previously calculated rotation,
                // then we add time by writing Time.fixedDeltaTime (fixed cuz we are in the method FixedUpdate)
                // and we multiply that by a rotationRate to make it go faster or slower.
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, rotation, Time.fixedDeltaTime * 100);
            }
        }

        private void StartAnimation(string x)
        {
            Debug.LogWarning("<color=cyan>Start Animation >>> " + x + "</color>");
            _animator.CrossFade(x, PlayerConst.AnimationCrossFade);
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
