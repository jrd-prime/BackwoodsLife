﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe;
using BackwoodsLife.Scripts.Data.Scriptable.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; private set; }
        public Dictionary<string, ItemSettings> ItemsConfigCache { get; } = new();
        public Dictionary<string, SRecipe> RecipesConfigCache { get; } = new();

        private SMainConfig _mainConfig;

        [Inject]
        private void Construct(SMainConfig mainConfig) =>
            _mainConfig = mainConfig;

        public void ServiceInitialization()
        {
            ConfigsCache = new Dictionary<Type, object>();

            AddToCache(_mainConfig.characterConfig);

            {
                // TODO refactor
                _mainConfig.Check(_mainConfig.GameItemsList.resourceItems, typeof(ResourceType), "Resource");
                _mainConfig.Check(_mainConfig.GameItemsList.foodItems, typeof(FoodType), "Food");
                _mainConfig.Check(_mainConfig.GameItemsList.toolItems, typeof(ToolType), "Tool");
                _mainConfig.Check(_mainConfig.GameItemsList.skillItems, typeof(SkillType), "Skill");

                _mainConfig.Check(_mainConfig.WorldItemsList.buildingItems, typeof(UsableAndUpgradableType), "Building");
                _mainConfig.Check(_mainConfig.WorldItemsList.collectableItems, typeof(CollectableType), "Collectable");
                _mainConfig.Check(_mainConfig.WorldItemsList.placeItems, typeof(UsableType), "Place");
            }

            //TODO refactor
            AddToItemsCache(_mainConfig.GameItemsList.resourceItems);
            AddToItemsCache(_mainConfig.GameItemsList.foodItems);
            AddToItemsCache(_mainConfig.GameItemsList.skillItems);
            AddToItemsCache(_mainConfig.GameItemsList.toolItems);
            AddToItemsCache(_mainConfig.WorldItemsList.placeItems);
            AddToItemsCache(_mainConfig.WorldItemsList.collectableItems);
            AddToItemsCache(_mainConfig.WorldItemsList.buildingItems);

            AddToRecipesCache(_mainConfig.recipeItemsList.recipeSimpleList);
            AddToRecipesCache(_mainConfig.recipeItemsList.recipeAdvancedList);
            AddToRecipesCache(_mainConfig.recipeItemsList.recipeExpertList);
        }

        private void AddToRecipesCache<T>(List<CustomRecipe<T>> items) where T : SRecipe
        {
            foreach (var item in items)
                RecipesConfigCache.Add(item.recipe.recipeData.returnedItem.item.itemName, item.recipe);
        }

        private void AddToItemsCache<T>(List<CustomItemConfig<T>> items) where T : ItemSettings
        {
            foreach (var item in items) ItemsConfigCache.Add(item.config.itemName, item.config);
        }

        private void AddToCache(object config)
        {
            if (ConfigsCache.TryAdd(config.GetType(), config)) Debug.Log($"Add to cache {config.GetType()}");
        }

        public AssetReferenceTexture2D GetIconReference(string elementTypeName)
        {
            return GetItemConfig<ItemSettings>(elementTypeName).iconReference;
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            return ConfigsCache[typeof(T)] as T;
        }

        public RecipeItemsList GetRecipeItemsList() => _mainConfig.recipeItemsList;

        public T GetItemConfig<T>(string elementTypeName) where T : ItemSettings
        {
            // Debug.LogWarning($"GetItemConfig {elementTypeName} / {typeof(T)}");
            if (!ItemsConfigCache.ContainsKey(elementTypeName))
                throw new KeyNotFoundException($"Config {elementTypeName} not found");

            return ItemsConfigCache[elementTypeName] as T;
        }
    }
}
