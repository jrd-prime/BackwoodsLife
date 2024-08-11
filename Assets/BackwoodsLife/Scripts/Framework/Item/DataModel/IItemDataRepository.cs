using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using R3;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel
{
    public interface IDontKnowRepository
    {
        public void Update(string name, int to);
    }

    public interface IItemDataRepository : IDataRepository
    {
        public bool AddItem(string itemName, int quantity);
        public bool AddItem(in List<ItemData> itemsData);
        public bool RemoveItem(string itemName, int quantity);
        public bool RemoveItem(List<ItemData> itemsData);
    }


    public interface IDataRepository : IInitializable
    {
        public ReactiveProperty<List<ItemDataChanged>> OnRepositoryDataChanged { get; }
        public IReadOnlyDictionary<string, int> GetCacheData();
        public void SetItemsToInitialization(Dictionary<string, int> initItems);

        public ItemData GetItem(string itemName);

        public bool IsEnough(Dictionary<SItemConfig, int> itemsDictionary);
        public bool IsEnough(string itemName, int count);
        public bool IsEnough(KeyValuePair<SItemConfig, int> valuePair);
        public int GetValue(string typeNameItemName);
    }
}
