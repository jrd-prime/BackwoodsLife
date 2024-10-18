using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe;
using BackwoodsLife.Scripts.Data.Scriptable.Settings;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public interface IConfigManager : ILoadingOperation
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public Dictionary<string, ItemSettings> ItemsConfigCache { get; }
        public Dictionary<string, SRecipe> RecipesConfigCache { get; }
        public T GetItemConfig<T>(string elementTypeName) where T : ItemSettings;

        // public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfigNew;
        public AssetReferenceTexture2D GetIconReference(string elementTypeName);
        public T GetConfig<T>() where T : ScriptableObject;

        public IReadOnlyDictionary<string, ItemSettings> GetItemsCache() => ItemsConfigCache;
        public IReadOnlyDictionary<string, SRecipe> GetRecipeCache() => RecipesConfigCache;
        public IReadOnlyDictionary<Type, object> GetMainConfigCache() => ConfigsCache;

        public RecipeItemsList GetRecipeItemsList();
    }
}
