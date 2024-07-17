﻿using System;
using System.Collections.Generic;
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
        private void Construct(SMainConfigurationsList mainConfigurationsList) => _mainConfigurationsList = mainConfigurationsList;

        public void Initialize()
        {
            AddToCache(_mainConfigurationsList.staticInteractableObjectsList);
            AddToCache(_mainConfigurationsList.nonStaticInteractableObjectsList);
            AddToCache(_mainConfigurationsList.characterConfiguration);
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
