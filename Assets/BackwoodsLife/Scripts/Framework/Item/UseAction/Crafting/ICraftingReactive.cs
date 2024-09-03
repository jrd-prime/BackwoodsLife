using R3;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public interface ICraftingReactive : IUseActionReactive
    {
        public ReactiveProperty<RecipeInfoData> SelectedRecipePanelData { get; }
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData { get; }
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData { get; }
    }
}
