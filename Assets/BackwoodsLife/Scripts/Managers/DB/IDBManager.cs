using System;
using System.Collections.Generic;
using Game.Scripts.Boostrap;

namespace BackwoodsLife.Scripts.Managers.DB
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