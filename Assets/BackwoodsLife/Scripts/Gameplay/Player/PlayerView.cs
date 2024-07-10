using System;
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
        private float moveSpeed;
        private float rotationSpeed;

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;


        public Animator animator { get; set; }

        public Rigidbody rb { get; set; }

        public Vector3 moveDirection { get; set; }
        public Quaternion rot { get; set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = gameObject.GetComponent<Animator>();
            moveSpeed = _viewModel.MoveSpeed;
            rotationSpeed = _viewModel.RotationSpeed;


            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _viewModel.MoveDirection
                .Subscribe(newDirection => { moveDirection = newDirection; })
                .AddTo(_disposables);

            // _viewModel.PlayAnimationByName
            //     .Skip(1)
            //     // .DistinctUntilChanged()
            //     .Subscribe(x =>
            //     {
            //         Debug.LogWarning("<color=cyan>Start Animation >>> " + x + "</color>");
            //         animator.CrossFade(x, PlayerConst.AnimationCrossFade);
            //     })
            //     .AddTo(_disposables);
        }

        private void FixedUpdate()
        {
            if (moveDirection == Vector3.zero) return;
            Move();
        }

        private void Move()
        {
            rb.position += moveDirection * (moveSpeed * Time.fixedDeltaTime);
            _viewModel.SetModelPosition(rb.position);
            
            if (moveDirection.sqrMagnitude > 0)
            {
                var rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                rb.rotation = Quaternion.Lerp(rb.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
                _viewModel.SetModelRotation(rb.rotation);
            }
        }


        private void OnDestroy() => _disposables.Dispose();
    }
}
