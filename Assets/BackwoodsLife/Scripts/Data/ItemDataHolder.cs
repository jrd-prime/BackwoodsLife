using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data
{
    public abstract class ItemDataHolder
    {
        protected virtual Dictionary<string, int> ItemsCache { get; set; }

        public abstract void Initialize();

        public virtual void AddItem(string name, int count)
        {
        }

        public virtual void RemoveItem(string name, int count)
        {
        }

        public virtual ItemData GetItem(string name)
        {
            Debug.LogWarning("Get item " + name);
            return new ItemData
            {
                Name = name,
                Count = ItemsCache[name]
            };
        }
    }
}
