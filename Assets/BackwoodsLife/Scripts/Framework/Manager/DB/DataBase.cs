using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.DB
{
    public class DataBase : IDataBase
    {
        public UniTask<bool> Connect()
        {
            Debug.LogWarning($"DB fake connect");
            return UniTask.FromResult(true);
        }

        public bool HasPlayerId(string playerId)
        {
            Debug.LogWarning(" DB HasPlayerId()");
            return true;
        }
    }
}
