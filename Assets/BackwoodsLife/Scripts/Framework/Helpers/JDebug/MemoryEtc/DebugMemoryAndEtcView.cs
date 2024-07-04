using BackwoodsLife.Scripts.Framework.Player;
using BackwoodsLife.Scripts.Framework.Scope;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Helpers.JDebug.MemoryEtc
{
    public class DebugMemoryAndEtcView : MonoBehaviour
    {
        private DebugMemoryAndEtcViewModel _viewModel;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(DebugMemoryAndEtcViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            var root = gameObject.GetComponent<UIDocument>().rootVisualElement;

            var mono = root.Q<Label>("mono");
            mono.text = "default";
            var graph = root.Q<Label>("graph");
            graph.text = "default";
            var btnFollow = root.Q<Button>("btn-follow");
            btnFollow.text = "Follow/Unfollow";

            _viewModel.MonoMemoryUsageView.Subscribe(x => mono.text = $"Heap: {x} MB").AddTo(_disposable);
            _viewModel.GraphicsMemoryUsageView.Subscribe(x => graph.text = $"Graphics: {x} MB").AddTo(_disposable);

            btnFollow.clicked += () =>
            {
                var player = FindObjectOfType<GameSceneContext>().GetComponent<GameSceneContext>();
                var a = player.Container.Resolve<IPlayerViewModel>();
                _viewModel.FollowTarget(a);
            };
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
