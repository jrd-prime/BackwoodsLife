using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using R3;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel
{
    public abstract class DataRepository : IDataRepository
    {
        // TODO load saved data and initialize
        public ReactiveProperty<List<ItemDataChanged>> OnRepositoryDataChanged { get; } = new();

        public IReadOnlyDictionary<string, int> GetCacheData() => ItemsCache;

        public void SetItemsToInitialization(Dictionary<string, int> initItems)
        {
            ItemsCache = initItems;
            TempListForDataChanges.Clear();
            TempListForDataChanges.AddRange(initItems
                .Select(item => new ItemDataChanged { Name = item.Key, From = 0, To = item.Value })
                .ToList());

            RepositoryDataChanged();
        }

        protected void RepositoryDataChanged()
        {
            OnRepositoryDataChanged.Value = TempListForDataChanges;
            OnRepositoryDataChanged.ForceNotify();
            TempListForDataChanges.Clear();
        }

        protected void CheckItem(string name)
        {
            if (!ItemsCache.ContainsKey(name))
                throw new KeyNotFoundException($"\"{name}\" not found in ItemsCache. Check config name or enum");
        }

        public ItemData GetItem(string itemName)
        {
            CheckItem(itemName);
            return new ItemData { Name = itemName, Quantity = ItemsCache[itemName] };
        }

        public int GetValue(string typeNameItemName)
        {
            CheckItem(typeNameItemName);
            return ItemsCache[typeNameItemName];
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

        protected Dictionary<string, int> ItemsCache { get; set; } = new();

        /// <summary>
        /// Используется для сбора данных. После оповещения об изменениях - очищается
        /// </summary>
        protected readonly List<ItemDataChanged> TempListForDataChanges = new();

        public abstract void Initialize();
    }
}
