// using Game.Scripts.Helpers;

using BackwoodsLife.Scripts.Data.LoadingScreen;
using BackwoodsLife.UI;
using R3;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.LoadingScreen
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

            var header = _root.Q<Label>("header-label");

            // Подписываемся на вьюмодель
            _viewModel.HeaderView.Subscribe(x => header.text = x);
        }
    }
}
