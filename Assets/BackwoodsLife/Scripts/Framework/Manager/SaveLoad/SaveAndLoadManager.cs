using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.DB;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.SaveLoad
{
    public sealed class SaveAndLoadManager : ISaveAndLoadManager
    {
        public string Description => "SaveAndLoadManager";
        public string PlayerId { get; private set; }

        private IDBManager _dbManager;
        private Dictionary<Type, object> _cache = new();

        [Inject]
        private void Construct(IDBManager dbManager) => _dbManager = dbManager;

        public void ServiceInitialization()
        {
            Assert.IsNotNull(_dbManager, "DBManager is null!");
            Assert.IsTrue(_dbManager.IsInitialized,
                "DBManager is not initialized. Ensure that it is initialized before SaveAndLoadManager");

            // Get or create the player identifier on the current device in PlayerPrefs
            PlayerId = PlayerPrefsHelper.GetOrSetPlayerId(PlayerConst.PlayerIdPrefsKey);

            /*
             * Initialize the service.
             * Load data from the database.
             * If this is the first launch, what should we do?
             */

            // If there is data in the database for the identifier, load it. If not, generate a record for the current identifier
            _cache = _dbManager.IsHasDataFor(PlayerId)
                ? _dbManager.LoadAllData(PlayerId)
                : _dbManager.GenerateAndGetForPlayerId(PlayerId);
        }

        public void Load()
        {
            Debug.LogWarning("SaveAndLoadManager Load()");
        }

        /// <summary>
        /// Returns an object from its cache. An object that was loaded from the database during the service initialization.
        /// </summary>
        public T Get<T>() where T : IData => (T)_cache[typeof(T)];

        public void Set<T>(object instance) where T : IData
        {
            _cache.TryAdd(typeof(T), instance);
            Debug.LogWarning("Saved to cache: " + typeof(T).Name);
        }

        public void Set(object instance)
        {
            _cache.TryAdd(instance.GetType(), instance);
            Debug.LogWarning("Saved to cache: " + instance.GetType().Name);
        }

    }
}
