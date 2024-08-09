using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
{
    /// <summary>
    /// Отвечает за хранение данных 
    /// </summary>
    public abstract class ItemDataRepository : IItemDataRepository
    {
        // TODO load saved data and initialize
        public ReactiveProperty<List<ItemDataChanged>> OnRepositoryDataChanged { get; } = new();
        protected Dictionary<string, int> ItemsCache { get; set; } = new();

        /// <summary>
        /// Используется для сбора данных. После оповещения об изменениях - очищается
        /// </summary>
        protected readonly List<ItemDataChanged> TempListForDataChanges = new();

        public abstract void Initialize();

        public bool AddItem(string itemName, int quantity)
        {
            CheckItem(itemName);
            var currentAmount = ItemsCache[itemName];
            Debug.LogWarning($"Add: {quantity} {itemName}");

            var newAmount = ItemsCache[itemName] += quantity;
            var changed = new ItemDataChanged { Name = itemName, From = currentAmount, To = newAmount };

            TempListForDataChanges.Add(changed);
            RepositoryDataChanged();
            return true;
        }

        public bool AddItem(in List<ItemData> itemsData)
        {
            foreach (var itemData in itemsData)
            {
                CheckItem(itemData.Name);
                var currentAmount = ItemsCache[itemData.Name];
                Debug.LogWarning($"Add: {itemData.Quantity} {itemData.Name}");

                TempListForDataChanges.Add(new ItemDataChanged
                {
                    Name = itemData.Name, From = currentAmount, To = ItemsCache[itemData.Name] += itemData.Quantity
                });
            }

            RepositoryDataChanged();
            return true;
        }

        public bool RemoveItem(string itemName, int quantity)
        {
            CheckItem(itemName);
            var currentAmount = ItemsCache[itemName];
            Debug.LogWarning($"Remove: {quantity} {itemName}");

            var newAmount = ItemsCache[itemName] -= quantity;
            var changed = new ItemDataChanged { Name = itemName, From = currentAmount, To = newAmount };

            TempListForDataChanges.Add(changed);
            RepositoryDataChanged();
            return true;
        }

        public bool RemoveItem(List<ItemData> itemsData)
        {
            foreach (var itemData in itemsData)
            {
                CheckItem(itemData.Name);
                var currentAmount = ItemsCache[itemData.Name];
                Debug.LogWarning($"Remove: {itemData.Quantity} {itemData.Name}");

                var newAmount = ItemsCache[itemData.Name] -= itemData.Quantity;

                TempListForDataChanges.Add(new ItemDataChanged
                    { Name = itemData.Name, From = currentAmount, To = newAmount });
            }

            RepositoryDataChanged();
            return true;
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
            TempListForDataChanges.Clear();
            TempListForDataChanges.AddRange(initItems
                .Select(item => new ItemDataChanged { Name = item.Key, From = 0, To = item.Value })
                .ToList());

            RepositoryDataChanged();
        }

        public IReadOnlyDictionary<string, int> GetCacheData() => ItemsCache;

        protected void RepositoryDataChanged()
        {
            OnRepositoryDataChanged.Value = TempListForDataChanges;
            OnRepositoryDataChanged.ForceNotify();
            TempListForDataChanges.Clear();
        }

        private void CheckItem(string name)
        {
            if (!ItemsCache.ContainsKey(name))
                throw new KeyNotFoundException($"\"{name}\" not found in ItemsCache. Check config name or enum");
        }
    }

    public record ItemDataChanged
    {
        public string Name;
        public int From;
        public int To;
    }
}
