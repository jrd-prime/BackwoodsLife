using System;
using System.Collections.Generic;
using Game.Scripts.Managers.DataBase;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Managers.DB
{
    public class DBManager : IDBManager
    {
        private IDataBase _dataBase;

        public bool IsHasData { get; private set; } //TODO remove?
        public bool IsInitialized { get; private set; }


        [Inject]
        private void Construct(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public bool IsHasDataFor(string playerId) => _dataBase.HasPlayerId(playerId);


        public Dictionary<Type, object> GenerateAndGetForPlayerId(string playerId)
        {
            Debug.LogWarning("DefaultDBManager GenerateAndGetForPlayerId()");
            return null;
        }


        public async void ServiceInitialization()
        {
            var isDbConnected = await _dataBase.Connect();

            // конект к дб
            if (isDbConnected)
            {
                Debug.LogWarning("db connected");
            }
            else
            {
                Debug.LogWarning("db not connected");
            }

            IsInitialized = true;
        }

        public string Description => "DefaultDBManager";

        public Dictionary<Type, object> LoadAllData(string playerId)
        {
            // TODO REMOVE
            Debug.LogWarning("RETURN FAKE DATA FROM " + this);
            return new Dictionary<Type, object>
            {
                // {
                    // typeof(PlayerModel), new PlayerModel
                    // {
                    //     Position = new ReactiveProperty<Vector3>(new Vector3(-4, 0, -4)),
                    //     Rotation = new ReactiveProperty<Vector3>(new Vector3(0, -135, 0))
                    // }
                // }
            };
        }
    }
}