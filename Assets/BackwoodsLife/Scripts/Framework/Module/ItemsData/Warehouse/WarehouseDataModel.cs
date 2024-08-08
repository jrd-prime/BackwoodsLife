using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using R3;
using UnityEngine;
using ItemData = BackwoodsLife.Scripts.Data.Common.Records.ItemData;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse
{
    public class WarehouseItemDataModel : ItemDataRepository
    {
        public override void Initialize()
        {
            // Debug.LogWarning("Warehouse data init");
            ItemsCache = new Dictionary<string, int>();

            // TODO load saved data and initialize

            // List of enums that can be stored in the warehouse
            List<Type> list = new() { typeof(EResource), typeof(EFood) };

            // Init to zero
            foreach (var name in list.SelectMany(Enum.GetNames))
                ItemsCache.TryAdd(name, 0);

            // TODO think about it

            OneMoreList.Clear();

            foreach (var item in ItemsCache)
            {
                OneMoreList.Add(new ItemData { Name = item.Key, Quantity = item.Value });
                // Debug.LogWarning($"Add {item.Key} {item.Value}");
            }


            InventoryChanged(OneMoreList);
        }

        public ReactiveProperty<List<ItemData>> OnInventoryChanged = new();

        public void IncreaseResource(string itemName, int quantity)
        {
            AddItem(itemName, quantity);

            OneMoreList.Add(ToItemData(itemName, quantity));
            InventoryChanged(OneMoreList);
        }

        public void IncreaseResource(List<ItemData> elements)
        {
            foreach (var element in elements)
            {
                if (!ItemsCache.TryGetValue(element.Name, out var currentAmount))
                {
                    throw new Exception(
                        $"Not found {element.Name} in warehouse cache. Check that element added to EResource or EFood");
                }

                Debug.LogWarning($"AddResource {element.Name} {element.Quantity}. Before {currentAmount}");

                ItemsCache[element.Name] += element.Quantity;
                OneMoreList.Add(element with { Quantity = ItemsCache[element.Name] });
            }

            InventoryChanged(OneMoreList);
        }


        public void DecreaseResource(string elementType, int amount)
        {
            RemoveItem(elementType, amount);
            OneMoreList.Add(ToItemData(elementType, amount));
            InventoryChanged(OneMoreList);
        }

        public void DecreaseResource(List<ItemData> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning(
                    $"AddResource {element.Name} {element.Quantity}. Before {ItemsCache[element.Name]}");
                ItemsCache[element.Name] += element.Quantity;
                OneMoreList.Add(element with { Quantity = ItemsCache[element.Name] });
            }

            InventoryChanged(OneMoreList);
        }

        public bool HasEnoughResource(List<ItemData> inventoryElements)
        {
            return inventoryElements.All(element => ItemsCache[element.Name] >= element.Quantity);
        }

        public bool HasEnoughResource(string objResourceType, int amount)
        {
            return ItemsCache[objResourceType] >= amount;
        }


        public Dictionary<string, int> GetInventoryData() => ItemsCache;
    }
}
