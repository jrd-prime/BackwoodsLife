using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
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
        public Dictionary<string, SItemConfig> ItemsConfigCache { get; } = new();

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
                _mainConfig.Check(_mainConfig.GameItemsList.resourceItems, typeof(EResource), "Resource");
                _mainConfig.Check(_mainConfig.GameItemsList.foodItems, typeof(EFood), "Food");
                _mainConfig.Check(_mainConfig.GameItemsList.toolItems, typeof(ETool), "Tool");
                _mainConfig.Check(_mainConfig.GameItemsList.skillItems, typeof(ESkill), "Skill");

                _mainConfig.Check(_mainConfig.WorldItemsList.buildingItems, typeof(EUseAndUpgradeName), "Building");
                _mainConfig.Check(_mainConfig.WorldItemsList.collectableItems, typeof(ECollectName), "Collectable");
                _mainConfig.Check(_mainConfig.WorldItemsList.placeItems, typeof(EUseName), "Place");
            }

            //TODO refactor
            AddToItemsCache(_mainConfig.GameItemsList.resourceItems);
            AddToItemsCache(_mainConfig.GameItemsList.foodItems);
            AddToItemsCache(_mainConfig.GameItemsList.skillItems);
            AddToItemsCache(_mainConfig.GameItemsList.toolItems);
            AddToItemsCache(_mainConfig.WorldItemsList.placeItems);
            AddToItemsCache(_mainConfig.WorldItemsList.collectableItems);
            AddToItemsCache(_mainConfig.WorldItemsList.buildingItems);
        }

        private void AddToItemsCache<T>(List<CustomItemConfig<T>> items) where T : SItemConfig
        {
            foreach (var item in items) ItemsConfigCache.Add(item.config.itemName, item.config);
        }

        private void AddToCache(object config)
        {
            if (ConfigsCache.TryAdd(config.GetType(), config)) Debug.Log($"Add to cache {config.GetType()}");
        }

        public AssetReferenceTexture2D GetIconReference(string elementTypeName)
        {
            return GetItemConfig<SItemConfig>(elementTypeName).iconReference;
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            return ConfigsCache[typeof(T)] as T;
        }

        public T GetItemConfig<T>(string elementTypeName) where T : SItemConfig
        {
            // Debug.LogWarning($"GetItemConfig {elementTypeName} / {typeof(T)}");
            if (!ItemsConfigCache.ContainsKey(elementTypeName))
                throw new KeyNotFoundException($"Config {elementTypeName} not found");

            return ItemsConfigCache[elementTypeName] as T;
        }
    }
}
