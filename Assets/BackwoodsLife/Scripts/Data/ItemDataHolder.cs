using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data
{
    public abstract class ItemDataHolder : IInitializable
    {
        protected Dictionary<string, int> ItemsCache { get; set; }

        public abstract void Initialize();

        public void AddItem(string name, int count)
        {
            Debug.LogWarning($"AddResource {name} {count}. Before {ItemsCache[name]}");
            CheckItem(name);
            ItemsCache[name] += count;
            Debug.LogWarning($"After {ItemsCache[name]}");
        }

        public void RemoveItem(string name, int count)
        {
            Debug.LogWarning($"RemoveResource {name} {count}. Before {ItemsCache[name]}");
            CheckItem(name);
            // TODO check if -=  >= 0
            ItemsCache[name] -= count;
            Debug.LogWarning($"After {ItemsCache[name]}");
        }


        public ItemData GetItem(string itemName)
        {
            CheckItem(itemName);
            return new ItemData { Name = itemName, Count = ItemsCache[itemName] };
        }

        public bool IsEnough(Dictionary<SItemConfig, int> itemsDictionary)
        {
            var result = true;
            foreach (var _ in itemsDictionary
                         .Where(item => !IsEnough(item.Key.itemName, item.Value))) result = false;
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


        private void CheckItem(string name)
        {
            if (!ItemsCache.ContainsKey(name))
                throw new KeyNotFoundException($"\"{name}\" not found in ItemsCache. Check config name or enum");
        }
    }
}
