using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Scriptable.Configuration;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; } = new();
        private SMainConfigurations _mainConfigurations;

        [Inject]
        private void Construct(SMainConfigurations mainConfigurations) => _mainConfigurations = mainConfigurations;

        public void Initialize()
        {
            AddToCache(_mainConfigurations.staticInteractableObjectsList);
            AddToCache(_mainConfigurations.nonStaticInteractableObjectsList);
            AddToCache(_mainConfigurations.characterConfiguration);
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

        public void ServiceInitialization()
        {
        }
    }
}
