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

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = gameObject.GetComponent<Animator>();

            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _viewModel.MoveDirection.Subscribe(x => { dir = x; }).AddTo(_disposables);

            var fixdt = Time.fixedDeltaTime;
            Debug.Log(fixdt);

            Observable.Interval(TimeSpan.FromSeconds(fixdt))
                .Subscribe(_ =>
                {
                    Debug.Log("New position: " + _viewModel.MoveDirection.Value);
                    rb.position += _viewModel.MoveDirection.Value * 10f;
                    // Debug.Log($"{_joystick.MoveDirection.Value} di");
                    // _direction = _joystick.MoveDirection.Value;
                    // MoveDirection.Value = _joystick.MoveDirection.Value;
                    // Тут можно обновлять ваше состояние или выполнять другие действия
                })
                .AddTo(_disposables);


            // _viewModel.Position
            //     .Subscribe(delta =>
            //     {
            //         Debug.LogWarning("New position: " + delta);
            //         rb.position += delta;
            //     })
            //     .AddTo(_disposables);

            // _viewModel.Rotation
            //     .Skip(1)
            //     .Subscribe(rotation =>
            //     {
            //         Debug.LogWarning("New rotation: " + rotation);
            //         rb.rotation = rotation;
            //     })
            //     .AddTo(_disposables);
            //
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

        public Animator animator { get; set; }

        public Rigidbody rb { get; set; }

        // private void FixedUpdate()
        // {
        //     rb.position += dir * 0.1f;
        // }

        public Vector3 dir { get; set; }

        private void OnDestroy() => _disposables.Dispose();
    }
}
