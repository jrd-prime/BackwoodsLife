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
        public bool AddItem(in List<ItemDto> itemsData);
        public bool RemoveItem(string itemName, int quantity);
        public bool RemoveItem(List<ItemDto> itemsData);
    }


    public interface IDataRepository : IInitializable
    {
        public ReactiveProperty<List<ItemDataChanged>> OnRepositoryDataChanged { get; }
        public IReadOnlyDictionary<string, int> GetCacheData();
        public void SetItemsToInitialization(Dictionary<string, int> initItems);

        public ItemDto GetItem(string itemName);

        public bool IsEnough(Dictionary<ItemSettings, int> itemsDictionary);
        public bool IsEnough(string itemName, int count);
        public bool IsEnough(KeyValuePair<ItemSettings, int> valuePair);
        public int GetValue(string typeNameItemName);
    }
}
