using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Player;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.DB
{
    public class DBManager : IDBManager
    {
        public string Description => "DefaultDBManager";
        public bool IsInitialized { get; private set; }
        public bool IsHasData { get; private set; } //TODO remove?

        private IDataBase _dataBase;

        [Inject]
        private void Construct(IDataBase dataBase) => _dataBase = dataBase;

        public bool IsHasDataFor(string playerId) => _dataBase.HasPlayerId(playerId);

        public Dictionary<Type, object> GenerateAndGetForPlayerId(string playerId)
        {
            Debug.LogWarning("DefaultDBManager GenerateAndGetForPlayerId()");
            return null;
        }

        public async void ServiceInitialization()
        {
            if (await _dataBase.Connect())
                IsInitialized = true;
            else
                throw new Exception("<color=red>DB not connected</color>");
        }

        public Dictionary<Type, object> LoadAllData(string playerId)
        {
            Debug.LogWarning("Return fake data from DB. PlayerId: " + playerId); // TODO remove

            var fakeData = new Dictionary<Type, object>();
            var fakePlayerModel = new PlayerModel();
            fakePlayerModel.SetPosition(new Vector3(-4, 0, -4));
            fakePlayerModel.SetRotation(new Quaternion());

            fakeData.Add(typeof(PlayerModel), fakePlayerModel);

            return fakeData;
        }
    }
}
