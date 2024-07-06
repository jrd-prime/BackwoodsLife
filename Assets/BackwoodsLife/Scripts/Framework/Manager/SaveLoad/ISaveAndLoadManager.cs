using BackwoodsLife.Scripts.Framework.Bootstrap;

namespace BackwoodsLife.Scripts.Framework.Manager.SaveLoad
{
    public interface ISaveAndLoadManager : ILoadingOperation
    {
        void Load();
        public T Get<T>() where T : IData;
        public void Set<T>(object instance) where T : IData;
        public void Set(object instance);
    }
}
