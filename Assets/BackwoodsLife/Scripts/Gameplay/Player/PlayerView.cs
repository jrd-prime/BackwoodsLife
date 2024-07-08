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

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            
            
            _animator = gameObject.GetComponent<Animator>();

            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _viewModel.PlayerPosition
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

        private void PositionHandler(Vector3 position) =>
            transform.position = new Vector3(position.x, transform.position.y, position.z);

        private void RotationHandler(Quaternion position) => transform.rotation = position;


        private void StartAnimation(string x)
        {
            Debug.LogWarning("<color=cyan>Start Animation >>> " + x + "</color>");
            _animator.CrossFade(x, PlayerConst.AnimationCrossFade);
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
