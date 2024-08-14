using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Gameplay.UI;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionViewBase : UIView, IUseActionView
    {
    }

    public abstract class CustomUseActionViewBase<TViewModel> : UseActionViewBase
        where TViewModel : UseActionViewModelBase
    {
        protected TViewModel ViewModel;

        [SerializeField] protected VisualTreeAsset MainTemplate;
        protected UIFrameController _uiFrameController;
        protected TemplateContainer _mainView;

        [Inject]
        private void Construct(TViewModel viewModel, UIFrameController uiFrameController)
        {
            Debug.LogWarning("Custom use action view construct with: " + viewModel.Description);
            ViewModel = viewModel;
            _uiFrameController = uiFrameController;
        }

        public void Show(TemplateContainer templateContainer)
        {
            _uiFrameController.ShowMainPopUpWindow(templateContainer);
            Debug.LogWarning("<color=green>" + ViewModel.Description + "</color>");
        }

        private void Awake()
        {
            _mainView = MainTemplate.Instantiate();

            ViewModel.PanelToShow
                .Skip(1)
                .Subscribe(x => { Show(x); })
                .AddTo(new CompositeDisposable());
        }
    }
}
