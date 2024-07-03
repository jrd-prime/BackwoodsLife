using Cysharp.Threading.Tasks;
using Game.Scripts.Managers.DataBase;
using UnityEngine;

namespace BackwoodsLife.Scripts.Managers.DB
{
    public class DataBase : IDataBase
    {
        public UniTask<bool> Connect()
        {
            Debug.LogWarning("DefaultDataBase FAKE Connect");
            return UniTask.FromResult(true);
        }

        public bool HasPlayerId(string playerId)
        {
            Debug.LogWarning(" DefaultDataBase HasPlayerId()");
            return true;
        }
    }
}