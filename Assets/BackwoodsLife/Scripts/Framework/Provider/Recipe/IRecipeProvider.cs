using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Provider.Recipe
{
    public interface IRecipeProvider : ILoadingOperation
    {
        public IReadOnlyDictionary<string, SRecipe> GetAllRecipes();
        public IReadOnlyDictionary<string, SRecipeSimple> GetAllSimpleRecipes();
        public IReadOnlyDictionary<string, SRecipeAdvanced> GetAllAdvancedRecipes();
        public IReadOnlyDictionary<string, SRecipeExpert> GetAllExpertRecipes();
        public T GetRecipe<T>(string elementTypeName) where T : SRecipe;
        public SRecipe GetRecipeByName(string recipeName);
    }
}
