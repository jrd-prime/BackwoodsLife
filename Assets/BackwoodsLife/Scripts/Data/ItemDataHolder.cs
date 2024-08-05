using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;

namespace BackwoodsLife.Scripts.Data
{
    public abstract class ItemDataHolder
    {
        protected Dictionary<string, int> ItemsCache { get; set; }

        public abstract void Initialize();

        public virtual void AddItem(string name, int count)
        {
        }

        public virtual void RemoveItem(string name, int count)
        {
        }

        public ItemData GetItem(string itemName)
        {
            if (!ItemsCache.TryGetValue(itemName, out var value))
                throw new KeyNotFoundException($"\"{itemName}\" not found in ItemsCache. Check config name or enum");

            return new ItemData { Name = itemName, Count = value };
        }

        public bool IsEnough(Dictionary<SItemConfig, int> itemsDictionary)
        {
            var result = true;
            foreach (var _ in itemsDictionary
                         .Where(item => !IsEnough(item.Key.itemName, item.Value)))
            {
                result = false;
            }

            return result;
        }

        public bool IsEnough(string itemName, int count)
        {
            return ItemsCache.ContainsKey(itemName) && ItemsCache[itemName] >= count;
        }

        public bool IsEnough(KeyValuePair<SItemConfig, int> valuePair)
        {
            return IsEnough(valuePair.Key.itemName, valuePair.Value);
        }
    }
}
