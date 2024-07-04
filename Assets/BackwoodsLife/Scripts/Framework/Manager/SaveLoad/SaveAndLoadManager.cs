using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.DB;
using Game.Scripts.Player.Const;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Managers.SaveLoad
{
    public sealed class SaveAndLoadManager : ISaveAndLoadManager
    {
        public string PlayerId { get; private set; }

        private IDBManager _dbManager;
        private Dictionary<Type, object> _cache = new();

        [Inject]
        private void Construct(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void ServiceInitialization()
        {
            Assert.IsNotNull(_dbManager, "DBManager is null!");
            Assert.IsTrue(_dbManager.IsInitialized, "DBManager is not initialized");

            // Получаем или создаем идентификатор игрока на текущем устройстве в префсах
            PlayerId = PlayerPrefsHelper.GetOrSetPlayerId(PlayerConst.PlayerIdPrefsKey);

            /*
             * Инициализируем сервис.
             * Загружаем данные из базы данных
             * Если это первый запуск, то что делаем?
             */

            // Если есть данные в базе по идентификатору, то загружаем их
            if (_dbManager.IsHasDataFor(PlayerId))
            {
                Debug.LogWarning("DataBase has data for " + PlayerId);
                _cache = _dbManager.LoadAllData(PlayerId);
            }
            else
            {
                // Если нету, то генерируем запись для текущего идентификатора
                Debug.LogWarning("DataBase has no data for " + PlayerId);
                _cache = _dbManager.GenerateAndGetForPlayerId(PlayerId);
            }
        }

        public string Description => "SaveAndLoadManager";

        public void Load()
        {
            Debug.LogWarning("SaveAndLoadManager Load()");
        }

        /// <summary>
        /// Возвращает объект из своего кэша. Объект, который был загружен из базы данных при инициализации сервиса 
        /// </summary>
        public T Get<T>() where T : IData
        {
            return (T)_cache[typeof(T)];
        }
    }
}