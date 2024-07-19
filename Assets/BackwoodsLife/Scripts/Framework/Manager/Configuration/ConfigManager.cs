using System;
using System.Collections.Generic;
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

        private SMainConfigurationsList _mainConfigurationsList;

        [Inject]
        private void Construct(SMainConfigurationsList mainConfigurationsList) =>
            _mainConfigurationsList = mainConfigurationsList;

        public void ServiceInitialization()
        {
            ConfigsCache = new Dictionary<Type, object>();
            AddToCache(_mainConfigurationsList.gameItemsList);
            AddToCache(_mainConfigurationsList.worldItemsList);
            AddToCache(_mainConfigurationsList.characterConfiguration);
        }

        private void AddToCache(object config)
        {
            if (ConfigsCache.TryAdd(config.GetType(), config)) Debug.Log($"Add to cache {config.GetType()}");
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            if (!ConfigsCache.ContainsKey(typeof(T)))
                throw new KeyNotFoundException($"Config {typeof(T)} not found");

            return (T)ConfigsCache[typeof(T)];
        }

        public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfig
        {
            var worldItemsList = ConfigsCache[typeof(SWorldItemsList)] as SWorldItemsList;
            if (worldItemsList == null)
                throw new Exception(nameof(SWorldItemsList) + " not found in " + nameof(ConfigsCache));

            var itemConfig = worldItemsList.ConfigsCache[enumTypeName] as T;


            if (itemConfig == null) throw new NullReferenceException("Config not found for: " + enumTypeName);

            Debug.LogWarning("GetGameItemConfig by name: " + enumTypeName + " / " + itemConfig.itemName);

            return itemConfig;
        }

        public AssetReferenceTexture2D GetIconReference(string elementTypeName)
        {
            var iconRef = GetConfig<SGameItemsList>();
            var b = iconRef.ConfigsCache[elementTypeName];
            return b.iconReference;
        }
    }
}
