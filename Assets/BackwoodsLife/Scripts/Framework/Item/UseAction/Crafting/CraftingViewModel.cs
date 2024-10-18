using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public sealed class CraftingViewModel : CustomUseActionViewModel<CraftingModel>, ICraftingReactive
    {
        public override string Description => "Crafting";
        public ReactiveProperty<RecipeInfoData> SelectedRecipePanelData => Model.SelectedRecipePanelData;
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData => Model.ItemsPanelData;
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData => Model.ProcessPanelData;
        public Action<string> OnRecipeButtonClicked { get; private set; }

        public override async void Activate(UseAndUpgradeItem itemConfig, Action onCompleteUseActionCallback)
        {
            Debug.LogWarning($"<color=red>Crafting action activated:</color> {itemConfig.itemName}");

            OnRecipeButtonClicked += RecipeButtonClicked;
            OnCompleteUseActionCallback = onCompleteUseActionCallback;
            // await UniTask.Delay(3333);
            Model.SetDataTo(itemConfig);
            IsPanelActive.Value = true;
        }

        private void RecipeButtonClicked(string recipeName)
        {
            Debug.LogWarning("RecipeButtonClicked callback in use action VIEW MODEL " + recipeName);
            Model.SetSelectedRecipe(recipeName);
        }

        public override void Deactivate()
        {
            Debug.LogWarning("<color=red>Crafting action deactivated</color>");
            OnRecipeButtonClicked -= RecipeButtonClicked;
            IsPanelActive.Value = false;
        }

        public override void OnCloseButtonClicked()
        {
            Debug.LogWarning("OnCloseButtonClicked callback in use action VIEW MODEL");
            Debug.LogWarning("OnCompleteUseActionCallback: " + OnCompleteUseActionCallback);
            OnCompleteUseActionCallback?.Invoke();
        }
    }
}
