using System;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
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
        protected readonly CompositeDisposable Disposables = new();

        protected abstract void InitializeElementsRefs();

        [Inject]
        private void Construct(TViewModel viewModel, UIFrameController uiFrameController)
        {
            Debug.LogWarning("Custom use action view construct with: " + viewModel.Description);
            ViewModel = viewModel;
            UIFrameController = uiFrameController;
        }

        private void Awake()
        {
            Assert.IsNotNull(ViewModel, "ViewModel is null");
            Assert.IsNotNull(UIFrameController, "UIFrameController is null");

            UIFrameController.OnCloseButtonClicked += OnCloseButtonClicked;

            ViewModel.IsPanelActive
                .Skip(1)
                .Subscribe(Show)
                .AddTo(Disposables);

            ViewModel.DescriptionPanelData
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.LogWarning("<color=green>Use action view base. Desc panel</color>");
                    UIFrameController.SetDescriptionToPopUpWindow(x);
                })
                .AddTo(Disposables);

            InitializeElementsRefs();
        }

        protected void Show(bool s)
        {
            Debug.LogWarning("show = " + s);
            if (!s) return;
            if (Panel == null) throw new NullReferenceException("Panel is null");
            UIFrameController.ShowMainPopUpWindowWithScroll(Panel);
            Debug.LogWarning("<color=green>" + ViewModel.Description + "</color>");
        }

        protected virtual void OnCloseButtonClicked()
        {
            Debug.LogWarning("OnCloseButtonClicked callback in use action VIEW");
            ViewModel.OnCloseButtonClicked();
        }

        private void OnDestroy()
        {
            Disposables.Dispose();
            UIFrameController.OnCloseButtonClicked -= OnCloseButtonClicked;
        }
    }
}
