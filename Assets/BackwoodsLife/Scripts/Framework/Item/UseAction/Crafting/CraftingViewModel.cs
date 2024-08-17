using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public sealed class CraftingViewModel : CustomUseActionViewModel<CraftingModel>, ICraftingReactive
    {
        public ReactiveProperty<CraftingInfoPanelData> InfoPanelData => Model.InfoPanelData;
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData => Model.ItemsPanelData;
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData => Model.ProcessPanelData;
        public override string Description => "Crafting";

        public override void Activate(SUseAndUpgradeItem itemConfig, Action onCompleteUseActionCallback)
        {
            Debug.LogWarning($"Crafting action activated: {itemConfig.itemName}");

            OnCompleteUseActionCallback = onCompleteUseActionCallback;
            Assert.IsNotNull(MainTemplate, $"MainTemplate is null: {this}");
            IsPanelActive.Value = true;

            Model.SetDataTo(itemConfig);
            // interactZoneStateCallback.Invoke();

            // onCompleteUseActionCallback.Invoke();
        }

        public override void Deactivate()
        {
            Debug.LogWarning("Crafting action deactivated");
            IsPanelActive.Value = false;
        }

        public override void OnCloseButtonClicked()
        {
            Debug.LogWarning("OnCloseButtonClicked callback in use action VIEW MODEL");
            Debug.LogWarning(OnCompleteUseActionCallback + " ????? ");
            OnCompleteUseActionCallback?.Invoke();
        }
    }
}
