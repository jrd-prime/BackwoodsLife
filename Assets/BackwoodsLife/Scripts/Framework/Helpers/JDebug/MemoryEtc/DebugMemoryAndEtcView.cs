using BackwoodsLife.Scripts.Framework.Scope;
using BackwoodsLife.Scripts.Gameplay.Player;
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

        private Button _btnExit;

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

            _btnExit = root.Q<Button>("btn-exit");


            var lab3name = root.Q<Label>("3labname");
            var lab3val = root.Q<Label>("3labval");
            lab3name.text = "default";
            lab3val.text = "default";

            _viewModel.MonoMemoryUsageView.Subscribe(x => mono.text = $"Heap: {x} MB").AddTo(_disposable);
            _viewModel.GraphicsMemoryUsageView.Subscribe(x => graph.text = $"Graphics: {x} MB").AddTo(_disposable);

            _btnExit.clicked += () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            };
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
