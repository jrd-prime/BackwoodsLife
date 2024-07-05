using System;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Player;
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

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();

            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _viewModel.PlayerPosition
                .Skip(1)
                .Subscribe(PlayerPositionHandler)
                .AddTo(_disposables);

            _viewModel.PlayAnimationByName
                .Skip(1)
                // .DistinctUntilChanged()
                .Subscribe(StartAnimation)
                .AddTo(_disposables);
        }

        private void PlayerPositionHandler(Vector3 position)
        {
            throw new NotImplementedException();
        }

        private void FixedUpdate()
        {
            _viewModel.SetModelPosition(transform.position);
        }

        private void StartAnimation(string x)
        {
            Debug.LogWarning("<color=cyan>Start Animation >>> " + x + "</color>");
            _animator.CrossFade(x, PlayerConst.AnimationCrossFade);
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
