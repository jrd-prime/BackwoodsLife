using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Gameplay.UI;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionViewBase : UIView, IUseActionView
    {
    }

    public abstract class CustomUseActionViewBase<TViewModel> : UseActionViewBase
        where TViewModel : UseActionViewModelBase
    {
        protected TViewModel ViewModel;

        /// <summary>
        /// Set this template to the view model on awake
        /// </summary>
        [SerializeField] protected VisualTreeAsset MainTemplate;

        protected UIFrameController _uiFrameController;
        private FramePopUpWindow frame;

        [Inject]
        private void Construct(TViewModel viewModel, UIFrameController uiFrameController)
        {
            Debug.LogWarning("Custom use action view construct with: " + viewModel.Description);
            ViewModel = viewModel;
            _uiFrameController = uiFrameController;
        }

        private void Awake()
        {
            frame = _uiFrameController.GetPopUpWindowFrame();
            ViewModel.PanelToShow
                .Skip(1)
                .Subscribe(x => { Show(x); })
                .AddTo(new CompositeDisposable());

            ViewModel.PanelDescription
                .Skip(1)
                .Subscribe(x => { FillDescription(x); })
                .AddTo(new CompositeDisposable());

            ViewModel.SetMainTemplate(MainTemplate);

            InitializeElementsRefs();
        }


        private void FillDescription(PanelDescriptionData panelDescriptionData)
        {
            frame.SetDescription(panelDescriptionData);
        }

        protected abstract void InitializeElementsRefs();

        public void Show(TemplateContainer templateContainer)
        {
            _uiFrameController.ShowMainPopUpWindowWithScroll(templateContainer);
            _uiFrameController.ShowMainPopUpWindow(templateContainer);
            Debug.LogWarning("<color=green>" + ViewModel.Description + "</color>");
        }
    }
}
