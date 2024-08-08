using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
{
    /// <summary>
    /// Отвечает за хранение данных 
    /// </summary>
    public abstract class ItemDataRepository : IItemDataRepository
    {
        public ReactiveProperty<List<ItemData>> OnItemsChanged { get; } = new();

        /// <summary>
        /// Initialized in <see cref="GameDataManager"/>
        /// </summary>
        protected Dictionary<string, int> ItemsCache { get; set; }

        protected readonly List<ItemData> TempList = new();
        protected readonly List<ItemData> OneMoreList = new();
        public abstract void Initialize();

        public void AddItem(string name, int count)
        {
            Debug.LogWarning($"AddResource {name} {count}. Before {ItemsCache[name]}");
            CheckItem(name);
            ItemsCache[name] += count;
            Debug.LogWarning($"After {ItemsCache[name]}");
        }

        public void AddItem(in List<ItemData> inventoryElements)
        {
            foreach (var element in inventoryElements)
            {
                Debug.LogWarning($"AddResource {element.Name} {element.Quantity}. Before {ItemsCache[element.Name]}");
                CheckItem(element.Name);
                ItemsCache[element.Name] += element.Quantity;
                Debug.LogWarning($"After {ItemsCache[element.Name]}");
            }
        }

        public void RemoveItem(string name, int count)
        {
            Debug.LogWarning($"RemoveResource {name} {count}. Before {ItemsCache[name]}");
            CheckItem(name);
            // TODO check if -=  >= 0
            ItemsCache[name] -= count;
            Debug.LogWarning($"After {ItemsCache[name]}");
        }

        public void RemoveItem(List<ItemData> inventoryElements)
        {
            foreach (var element in inventoryElements) RemoveItem(element.Name, element.Quantity);
        }


        public ItemData GetItem(string itemName)
        {
            CheckItem(itemName);
            return new ItemData { Name = itemName, Quantity = ItemsCache[itemName] };
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

        public void SetItemsToInitialization(Dictionary<string, int> initItems)
        {
            ItemsCache = initItems;

            var list = new List<ItemData>();
            foreach (var item in initItems)
            {
                list.Add(ToItemData(item.Key, item.Value));
            }

            InventoryChanged(list);
        }

        protected void InventoryChanged(List<ItemData> elements)
        {
            TempList.Clear();
            foreach (var element in elements)
            {
                TempList.Add(element);
            }

            OneMoreList.Clear();

            OnItemsChanged.Value = TempList;
            OnItemsChanged.ForceNotify();
        }

        private void CheckItem(string name)
        {
            if (!ItemsCache.ContainsKey(name))
                throw new KeyNotFoundException($"\"{name}\" not found in ItemsCache. Check config name or enum");
        }

        protected static ItemData ToItemData(string itemName, int quantity) =>
            new() { Name = itemName, Quantity = quantity };
    }
}
