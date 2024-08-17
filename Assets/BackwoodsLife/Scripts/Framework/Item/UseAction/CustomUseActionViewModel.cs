using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Framework.Item.UseAction.sort;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionViewModelBase : IUseActionViewModel, IUseActionReactive
    {
        public abstract ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; }
        public ReactiveProperty<bool> IsPanelActive { get; } = new();

        protected Action OnCompleteUseActionCallback;

        /// <summary>
        /// This template is set on awake in view
        /// </summary>
        protected VisualTreeAsset MainTemplate;

        public abstract string Description { get; }
        public abstract void Activate(SUseAndUpgradeItem itemConfig, Action onCompleteUseActionCallback);
        public abstract void Deactivate();

        public void SetMainTemplate(VisualTreeAsset mainTemplate) => MainTemplate = mainTemplate;

        public static IUseActionViewModel CreateUseAction(EUseType useType)
        {
            return useType switch
            {
                // EUseType.Cooking => new CookingActionViewModel(),
                // EUseType.PutItems => new PutActionViewModel(),
                // EUseType.TakeItems => new TakeActionViewModel(),
                EUseType.Crafting => new CraftingViewModel(),
                // EUseType.Resting => new RestingActionViewModel(),
                // EUseType.Fishing => new FishingActionViewModel(),
                // EUseType.Drinking => new DrinkingActionViewModel(),
                // EUseType.Eating => new EatingActionViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(useType), useType,
                    "Unknown use type! Need to add new use type")
            };
        }
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
