using System.Collections.Generic;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
{
    public abstract class DontKnowRepository : DataRepository
    {
        public void Update(string name, int to)
        {
            if (!ItemsCache.ContainsKey(name))
                throw new KeyNotFoundException($"\"{name}\" not found in ItemsCache. Check config name or enum");

            ItemsCache[name] = to;
        }
    }
}
