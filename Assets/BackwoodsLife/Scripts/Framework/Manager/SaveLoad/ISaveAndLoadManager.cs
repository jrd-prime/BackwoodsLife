using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;


namespace Game.Scripts.Managers.SaveLoad
{
    public interface ISaveAndLoadManager : ILoadingOperation
    {
        void Load();
        T Get<T>() where T : IData;
    }
}