// using Game.Scripts.Helpers;

using BackwoodsLife.Scripts.Gameplay.UI;
using R3;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    public class LoadingScreenView : UIView
    {
        private LoadingScreenViewModel _viewModel;
        private VisualElement _root;

        [Inject]
        private void Construct(LoadingScreenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Awake()
        {
            _root = gameObject.GetComponent<UIDocument>().rootVisualElement;
            // UIHelper.SetDefaultCanvasSize(ref _root);

#if UNITY_EDITOR
            _root.style.opacity = 0.1f;
#endif

            var header = _root.Q<Label>("header-label");

            // Подписываемся на вьюмодель
            _viewModel.HeaderView.Subscribe(x => header.text = x);
        }
    }
}
