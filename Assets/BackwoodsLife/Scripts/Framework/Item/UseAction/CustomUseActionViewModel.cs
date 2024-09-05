using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionViewModelBase : IUseActionViewModel, IUseActionReactive
    {
        public abstract ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; }
        public ReactiveProperty<bool> IsPanelActive { get; } = new();

        protected Action OnCompleteUseActionCallback;

        public abstract string Description { get; }
        public abstract void Activate(SUseAndUpgradeItem itemConfig, Action onCompleteUseActionCallback);
        public abstract void Deactivate();
        public abstract void OnCloseButtonClicked();
    }

    public struct PanelDescriptionData
    {
        public string Title;
        public string Description;
    }

    public abstract class CustomUseActionViewModel<TModel> : UseActionViewModelBase
        where TModel : class, IUseActionModelBase
    {
        protected TModel Model { get; private set; }

        public override ReactiveProperty<PanelDescriptionData> DescriptionPanelData => Model.DescriptionPanelData;

        [Inject]
        private void Construct(TModel model)
        {
            Debug.LogWarning("Use action view model construct");
            Model = model;
        }
    }
}
