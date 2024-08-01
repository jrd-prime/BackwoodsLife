using System.Collections.Generic;

namespace BackwoodsLife.Scripts.Data
{
    public abstract class ItemDataHolder
    {
        public virtual Dictionary<string, int> ItemsCache { get; protected set; }

        public virtual void AddItem(string name, int count)
        {
        }

        public virtual void RemoveItem(string name, int count)
        {
        }

        public virtual ItemData GetItem(string name)
        {
            return new ItemData();
        }
    }
}
