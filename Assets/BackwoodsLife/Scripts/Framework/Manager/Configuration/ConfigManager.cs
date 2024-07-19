using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Settings;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; } = new();
        private SMainConfigurationsList _mainConfigurationsList;

        [Inject]
        private void Construct(SMainConfigurationsList mainConfigurationsList) =>
            _mainConfigurationsList = mainConfigurationsList;

        public void Initialize()
        {
            AddToCache(_mainConfigurationsList.gameItemsList);
            AddToCache(_mainConfigurationsList.worldItemsList);
            AddToCache(_mainConfigurationsList.characterConfiguration);

            foreach (var o in ConfigsCache)
            {
                Debug.LogWarning($"In cache {o.Key}");
            }
        }

        private void AddToCache(object config)
        {
            ConfigsCache.TryAdd(config.GetType(), config);
        }


        public T GetConfig<T>() where T : ScriptableObject
        {
            if (ConfigsCache.ContainsKey(typeof(T)))
            {
                return (T)ConfigsCache[typeof(T)];
            }

            throw new KeyNotFoundException($"Config {typeof(T)} not found");
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

        public void ServiceInitialization()
        {
        }
    }
}
