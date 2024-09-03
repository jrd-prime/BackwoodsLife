using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe;
using BackwoodsLife.Scripts.Data.Scriptable.Settings;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Provider.Recipe
{
    public class RecipeProvider : IRecipeProvider
    {
        private IConfigManager _configManager;
        private IReadOnlyDictionary<string, SRecipe> _recipesLocalCache;
        private IReadOnlyDictionary<string, SRecipeSimple> _simpleRecipesLocalCache;
        private IReadOnlyDictionary<string, SRecipeAdvanced> _advancedRecipesLocalCache;
        private IReadOnlyDictionary<string, SRecipeExpert> _expertRecipesLocalCache;

        [Inject]
        private void Construct(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        public string Description => "Recipe Provider";

        public void ServiceInitialization()
        {
            Assert.IsNotNull(_configManager, "configManager is null");
            _recipesLocalCache = _configManager.GetRecipeCache();
            var recipeItemsList = _configManager.GetRecipeItemsList();
            _simpleRecipesLocalCache = RecipeListToDict(recipeItemsList.recipeSimpleList);
            _advancedRecipesLocalCache = RecipeListToDict(recipeItemsList.recipeAdvancedList);
            _expertRecipesLocalCache = RecipeListToDict(recipeItemsList.recipeExpertList);
        }

        private IReadOnlyDictionary<string, T> RecipeListToDict<T>(List<CustomRecipe<T>> recipeSimpleList)
            where T : SRecipe
        {
            return recipeSimpleList.ToDictionary(
                key => key.recipe.recipeData.returnedItem.item.itemName,
                value => value.recipe);
        }

        public IReadOnlyDictionary<string, SRecipe> GetAllRecipes() => _recipesLocalCache;

        public IReadOnlyDictionary<string, SRecipeSimple> GetAllSimpleRecipes() => _simpleRecipesLocalCache;

        public IReadOnlyDictionary<string, SRecipeAdvanced> GetAllAdvancedRecipes() => _advancedRecipesLocalCache;

        public IReadOnlyDictionary<string, SRecipeExpert> GetAllExpertRecipes() => _expertRecipesLocalCache;

        public T GetRecipe<T>(string elementTypeName) where T : SRecipe
        {
            if (!_recipesLocalCache.TryGetValue(elementTypeName, out var recipe))
                throw new KeyNotFoundException($"Recipe {elementTypeName} not found.");
            return recipe as T;
        }

        public SRecipe GetRecipeByName(string recipeName)
        {
            _recipesLocalCache.TryGetValue(recipeName, out SRecipe recipe);
            
            if (recipe == null) throw new KeyNotFoundException($"Recipe data for: {recipeName} - not found.");
            
            return recipe;
        }
    }
}
