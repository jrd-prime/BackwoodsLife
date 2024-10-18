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
    public sealed class CraftingModel : IUseActionModel<CraftingPanelData>, ICraftingReactive
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

        public PanelDescriptionData GetDescriptionData(WorldItemSettings worldItemSettings)
        {
            return new PanelDescriptionData { Title = worldItemSettings.itemName, Description = "crafting description" };
        }

        public CraftingPanelData GetMainPanelData()
        {
            return new CraftingPanelData();
        }

        public void SetDataTo(WorldItemSettings worldItemSettings)
        {
            DescriptionPanelData.Value = GetDescriptionData(worldItemSettings);
            SelectedRecipePanelData.Value = GetDefaultInfoPanelData();
            ItemsPanelData.Value = GetItemsPanelData(worldItemSettings);
            ProcessPanelData.Value = GetProcessPanelData(worldItemSettings);
        }

        private CraftingProcessPanelData GetProcessPanelData(WorldItemSettings worldItemSettings)
        {
            return new CraftingProcessPanelData
            {
            };
        }

        private CraftingItemsPanelData GetItemsPanelData(WorldItemSettings worldItemSettings)
        {
            var list = new List<CraftingItemData>();

            foreach (var recipe in _recipeProvider.GetAllRecipes())
            {
                list.Add(new CraftingItemData
                {
                    Title = recipe.Value.recipeData.returnedItem.item.itemName,
                    Icon = _assetProvider.GetIconFromRef(
                        _configManager.GetIconReference(recipe.Value.recipeData.returnedItem.item.itemName))
                });
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
