using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Scriptable;
using BackwoodsLife.Scripts.Framework.Scriptable.Configuration;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; } = new();


        [Inject]
        private void Construct(SMainConfigurations mainConfigurations)
        {
            Debug.LogWarning($"config manager construct + {mainConfigurations}");
            var staticInteractable = mainConfigurations.staticInteractableObjectsList;
            var nonStaticInteractable = mainConfigurations.nonStaticInteractableObjectsList;

            ConfigsCache.TryAdd(staticInteractable.GetType(), staticInteractable);
            ConfigsCache.TryAdd(nonStaticInteractable.GetType(), nonStaticInteractable);
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            if (ConfigsCache.ContainsKey(typeof(T)))
            {
                return (T)ConfigsCache[typeof(T)];
            }

            throw new KeyNotFoundException($"Config {typeof(T)} not found");
        }

        public T GetScriptableConfig<T>() where T : IConfigScriptable
        {
            if (ConfigsCache.TryGetValue(typeof(T), out var scriptableConfig))
            {
                return (T)scriptableConfig;
            }

            throw new KeyNotFoundException($"Config {typeof(T)} not found");
        }

        public void ServiceInitialization()
        {
            foreach (var config in ConfigsCache)
            {
                Debug.LogWarning(config.GetType());
            }
        }
    }
}
