using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;

namespace BackwoodsLife.Scripts.Framework.Manager.DB
{
    public interface IDBManager : ILoadingOperation
    {
        Dictionary<Type, object> LoadAllData(string playerId);
        bool IsHasData { get; }
        bool IsHasDataFor(string playerId);
        Dictionary<Type, object> GenerateAndGetForPlayerId(string playerId);
        public bool IsInitialized { get; }
    }
}