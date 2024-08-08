using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using R3;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
{
    public interface IItemDataRepository : IInitializable
    {
        public ReactiveProperty<List<ItemData>> OnItemsChanged { get; }
        public void AddItem(string name, int count);
        public void AddItem(in List<ItemData> inventoryElements);
        public void RemoveItem(string name, int count);
        public void RemoveItem(List<ItemData> inventoryElements);

        public ItemData GetItem(string itemName);

        public bool IsEnough(Dictionary<SItemConfig, int> itemsDictionary);
        public bool IsEnough(string itemName, int count);
        public bool IsEnough(KeyValuePair<SItemConfig, int> valuePair);

        public void SetItemsToInitialization(Dictionary<string, int> initItems);
    }
}
