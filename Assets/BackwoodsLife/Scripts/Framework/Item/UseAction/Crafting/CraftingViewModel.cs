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

        public override async void Activate(SUseAndUpgradeItem itemConfig, Action onCompleteUseActionCallback)
        {
            OnCompleteUseActionCallback = onCompleteUseActionCallback;
            Assert.IsNotNull(MainTemplate, $"MainTemplate is null: {this}");
            IsPanelActive.Value = true;

            Model.SetDataTo(itemConfig);
            // interactZoneStateCallback.Invoke();
            // await UniTask.Delay(3000);

            // onCompleteUseActionCallback.Invoke();
        }

        public override void Deactivate()
        {
            Debug.LogWarning("Crafting action deactivated");
            // IsPanelActive.Value = false;
            Debug.LogWarning(OnCompleteUseActionCallback + " ????? ");
            OnCompleteUseActionCallback?.Invoke();
        }
    }
}
