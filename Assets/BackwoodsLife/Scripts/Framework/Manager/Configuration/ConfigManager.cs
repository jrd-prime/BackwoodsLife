using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Settings;
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

        private SMainConfigurationsList _mainConfigurationsList;

        [Inject]
        private void Construct(SMainConfigurationsList mainConfigurationsList) =>
            _mainConfigurationsList = mainConfigurationsList;

        public void ServiceInitialization()
        {
            ConfigsCache = new Dictionary<Type, object>();
            AddToCache(_mainConfigurationsList.characterConfiguration);

            //TODO refactor
            AddToItemsCache(_mainConfigurationsList.GameItemsList.resourceItems);
            AddToItemsCache(_mainConfigurationsList.GameItemsList.foodItems);
            AddToItemsCache(_mainConfigurationsList.GameItemsList.skillItems);
            AddToItemsCache(_mainConfigurationsList.GameItemsList.toolItems);
            AddToItemsCache(_mainConfigurationsList.WorldItemsList.placeItems);
            AddToItemsCache(_mainConfigurationsList.WorldItemsList.collectableItems);
            AddToItemsCache(_mainConfigurationsList.WorldItemsList.buildingItems);
        }

        private void AddToItemsCache<T>(List<CustomItemConfig<T>> items) where T : SItemConfig
        {
            foreach (var item in items) ItemsConfigCache.Add(item.config.itemName, item.config);
        }

        private void AddToCache(object config)
        {
            if (ConfigsCache.TryAdd(config.GetType(), config)) Debug.Log($"Add to cache {config.GetType()}");
        }

        public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfigNew
        {
            var worldItemsList = ConfigsCache[typeof(SWorldItemsList)] as SWorldItemsList;
            if (worldItemsList == null)
                throw new Exception(nameof(SWorldItemsList) + " not found in " + nameof(ConfigsCache));

            if (!worldItemsList.ConfigsCache.TryGetValue(enumTypeName, out var itemConfig))
            {
                throw new NullReferenceException(
                    $"Config not found for: {enumTypeName}. Add {enumTypeName} config to WorldItemsList");
            }

            Debug.LogWarning("GetGameItemConfig by name: " + enumTypeName + " / " + itemConfig.itemName);

            return itemConfig as T;
        }

        public AssetReferenceTexture2D GetIconReference(string elementTypeName)
        {
            return GetItemConfig(elementTypeName).iconReference;
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            return ConfigsCache[typeof(T)] as T;
        }

        public SItemConfig GetItemConfig(string elementTypeName)
        {
            if (!ItemsConfigCache.ContainsKey(elementTypeName))
                throw new KeyNotFoundException($"Config {elementTypeName} not found");

            return ItemsConfigCache[elementTypeName];
        }
    }
}
