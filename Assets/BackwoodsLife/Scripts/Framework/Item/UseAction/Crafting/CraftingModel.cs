using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Provider.Recipe;
using R3;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingModel : IUseActionModel<CraftingPanelData>, ICraftingReactive
    {
        private IRecipeProvider _recipeProvider;
        public ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; } = new();
        public ReactiveProperty<CraftingInfoPanelData> InfoPanelData { get; } = new();
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData { get; } = new();
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData { get; } = new();

        [Inject]
        private void Construct(IRecipeProvider recipeProvider)
        {
            _recipeProvider = recipeProvider;
        }

        public PanelDescriptionData GetDescriptionData(SWorldItemConfig worldItemConfig)
        {
            return new PanelDescriptionData { Title = worldItemConfig.itemName, Description = "crafting description" };
        }

        public CraftingPanelData GetMainPanelData()
        {
            return new CraftingPanelData();
        }

        public void SetDataTo(SWorldItemConfig worldItemConfig)
        {
            DescriptionPanelData.Value = GetDescriptionData(worldItemConfig);
            InfoPanelData.Value = GetInfoPanelData(worldItemConfig);
            ItemsPanelData.Value = GetItemsPanelData(worldItemConfig);
            ProcessPanelData.Value = GetProcessPanelData(worldItemConfig);
        }

        private CraftingProcessPanelData GetProcessPanelData(SWorldItemConfig worldItemConfig)
        {
            return new CraftingProcessPanelData
            {
            };
        }

        private CraftingItemsPanelData GetItemsPanelData(SWorldItemConfig worldItemConfig)
        {
            // foreach (var VARIABLE in worldItemConfig.)
            // {
            // }

            var List = new List<CraftingItemData>();

            foreach (var recipe in _recipeProvider.GetAllRecipes())
            {
                List.Add(new CraftingItemData { Title = recipe.Value.recipeData.returnedItem.item.itemName });
            }


            return new CraftingItemsPanelData
            {
                Items = List
            };
        }

        private CraftingInfoPanelData GetInfoPanelData(SWorldItemConfig worldItemConfig)
        {
            return new CraftingInfoPanelData { Title = worldItemConfig.itemName, Description = "crafting description" };
        }
    }
}
