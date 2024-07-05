using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;

namespace BackwoodsLife.Scripts.Framework.Manager.SaveLoad
{
    public interface ISaveAndLoadManager : ILoadingOperation
    {
        void Load();
        T Get<T>() where T : IData;
    }
}