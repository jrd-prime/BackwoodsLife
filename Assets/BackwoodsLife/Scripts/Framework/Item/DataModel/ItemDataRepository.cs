using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel
{
    /// <summary>
    /// Отвечает за хранение данных 
    /// </summary>
    public abstract class ItemDataRepository : DataRepository, IItemDataRepository
    {
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
    }

    public record ItemDataChanged
    {
        public string Name;
        public int From;
        public int To;
    }
}
