using System;
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
        /// <summary>
        /// Set this template to the view model on awake
        /// </summary>
        [SerializeField] protected VisualTreeAsset mainTemplate;

        protected TViewModel ViewModel;
        protected UIFrameController UIFrameController;
        protected TemplateContainer Panel;

        protected abstract void InitializeElementsRefs();

        private FramePopUpWindow _frame;
        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(TViewModel viewModel, UIFrameController uiFrameController)
        {
            Debug.LogWarning("Custom use action view construct with: " + viewModel.Description);
            ViewModel = viewModel;
            UIFrameController = uiFrameController;
        }

        private void Awake()
        {
            Debug.LogWarning("Custom use action view AWAKE: " + ViewModel.Description);
            _frame = UIFrameController.GetPopUpWindowFrame();
            UIFrameController.OnCloseButtonClicked += OnCloseButtonClicked;

            ViewModel.IsPanelActive.Skip(1)
                .Subscribe(x => Show(x))
                .AddTo(Disposables);

            ViewModel.DescriptionPanelData
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.LogWarning("<color=green>Use action view base. Desc panel</color>");
                    FillDescription(x);
                })
                .AddTo(Disposables);

            
            
            ViewModel.SetMainTemplate(mainTemplate);

            InitializeElementsRefs();
        }

        private void FillDescription(PanelDescriptionData panelDescriptionData)
        {
            _frame.SetDescription(panelDescriptionData);
        }

        protected void Show(bool s)
        {
            Debug.LogWarning("show = " + s);
            if (!s) return;
            if (Panel == null) throw new NullReferenceException("Panel is null");
            UIFrameController.ShowMainPopUpWindowWithScroll(Panel);
            // UIFrameController.ShowMainPopUpWindow(templateContainer);
            Debug.LogWarning("<color=green>" + ViewModel.Description + "</color>");
        }

        protected virtual void OnCloseButtonClicked()
        {
            Debug.LogWarning("OnCloseButtonClicked callback in use action VIEW");
            Debug.LogWarning(ViewModel + " view model ");
            ViewModel.OnCloseButtonClicked();
        }

        private void OnDestroy()
        {
            Disposables.Dispose();
            UIFrameController.OnCloseButtonClicked -= OnCloseButtonClicked;
        }
    }
}
