using BackwoodsLife.Scripts.Gameplay.Player;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Manager.Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        private IPlayerViewModel _targetViewModel;
        private CompositeDisposable _subscribe = new();

        public UnityEngine.Camera MainCamera => mainCamera;

        public void SetFollowTarget(IPlayerViewModel target)
        {
            Debug.Log("SetFollowTarget " + target);

            if (_targetViewModel != null) _subscribe?.Dispose();
            SubscribeToTargetPosition(target);
        }

        public void RemoveTarget()
        {
            Debug.Log("RemoveFollowTarget " + _targetViewModel);
            _targetViewModel = null;
            _subscribe?.Dispose();
        }

        private void SubscribeToTargetPosition(IPlayerViewModel target)
        {
            _targetViewModel = target;
            _targetViewModel.Position
                .Subscribe(x => transform.DOMove(x, 1f).SetEase(Ease.OutQuad))
                .AddTo(_subscribe);
        }

        private void Awake()
        {
            Assert.IsNotNull(mainCamera, $"{nameof(mainCamera)} is null. {this}");
        }
    }
}
