using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using BackwoodsLife.Scripts.Framework.Provider.Recipe;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingModel : IUseActionModel<CraftingPanelData>, ICraftingReactive
    {
        private IRecipeProvider _recipeProvider;
        private IConfigManager _configManager;
        private IAssetProvider _assetProvider;

        public ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; } = new();
        public ReactiveProperty<RecipeInfoData> SelectedRecipePanelData { get; } = new();
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData { get; } = new();
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData { get; } = new();

        [Inject]
        private void Construct(IRecipeProvider recipeProvider, IConfigManager configManager,
            IAssetProvider assetProvider)
        {
            _recipeProvider = recipeProvider;
            _configManager = configManager;
            _assetProvider = assetProvider;
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
            SelectedRecipePanelData.Value = GetDefaultInfoPanelData();
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
            var list = new List<CraftingItemData>();

            foreach (var recipe in _recipeProvider.GetAllRecipes())
            {
                list.Add(new CraftingItemData { Title = recipe.Value.recipeData.returnedItem.item.itemName });
            }

            return new CraftingItemsPanelData
            {
                Items = list
            };
        }

        private RecipeInfoData GetDefaultInfoPanelData() => new RecipeInfoData { Title = "No recipe selected" };


        public void SetSelectedRecipe(string recipeName) =>
            SelectedRecipePanelData.Value = GetDataForRecipe(recipeName);


        private RecipeInfoData GetDataForRecipe(string recipeName)
        {
            Debug.LogWarning(recipeName + " recipe selected");

            var recipe = _recipeProvider.GetRecipeByName(recipeName);
            var iconReference = _configManager.GetIconReference(recipe.recipeData.returnedItem.item.itemName);
            var icon = _assetProvider.GetIconFromRef(iconReference);

            return new RecipeInfoData
            {
                Title = recipe.recipeData.returnedItem.item.itemName,
                Description = recipe.recipeData.description,
                Icon = icon,
                Ingredients = recipe.recipeData.ingredients
            };
        }
    }
}
