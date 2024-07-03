using Cysharp.Threading.Tasks;

namespace Game.Scripts.Managers.DataBase
{
    public interface IDataBase
    {
        // TODO CRUD
        UniTask<bool> Connect();
        bool HasPlayerId(string playerId);
    }
}