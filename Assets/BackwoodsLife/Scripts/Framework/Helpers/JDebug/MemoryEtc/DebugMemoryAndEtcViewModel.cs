using System;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Player;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Helpers.JDebug.MemoryEtc
{
    public class DebugMemoryAndEtcViewModel : IInitializable, IDisposable
    {
        public ReactiveProperty<int> MonoMemoryUsageView { get; } = new();
        public ReactiveProperty<int> GraphicsMemoryUsageView { get; } = new();

        private CompositeDisposable _disposable = new();
        private DebugMemoryAndEtcModel _model;
        private FollowSystem _followSystem;

        [Inject]
        private void Construct(DebugMemoryAndEtcModel model, FollowSystem followSystem)
        {
            _model = model;
            _followSystem = followSystem;
        }

        public void Initialize()
        {
            Debug.LogWarning("Init debug");
            _model.MonoMemory.Subscribe(x => MonoMemoryUsageView.Value = x).AddTo(_disposable);
            _model.GraphicsMemory.Subscribe(x => GraphicsMemoryUsageView.Value = x).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void FollowTarget(IPlayerViewModel playerViewModel) => _followSystem.SetTarget(playerViewModel);
    }
}
