using Cysharp.Threading.Tasks;

namespace BackwoodsLife.Scripts.Framework.Manager.DB
{
    public interface IDataBase
    {
        // TODO CRUD
        UniTask<bool> Connect();
        bool HasPlayerId(string playerId);
    }
}