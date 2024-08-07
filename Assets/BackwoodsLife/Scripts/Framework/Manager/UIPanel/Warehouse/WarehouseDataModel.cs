using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Structs;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse
{
    public class WarehouseDataModel : ItemDataHolder
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

            _oneMoreList.Clear();

            foreach (var item in ItemsCache)
            {
                _oneMoreList.Add(new InventoryElement { typeName = item.Key, Amount = item.Value });
                Debug.LogWarning($"Add {item.Key} {item.Value}");
            }


            InventoryChanged(_oneMoreList);
        }

        public ReactiveProperty<List<InventoryElement>> OnInventoryChanged = new();
        private List<InventoryElement> _tempList = new();
        private List<InventoryElement> _oneMoreList = new();

        private Dictionary<string, int> _warehouseItems = new();

        public void IncreaseResource(string elementType, int amount)
        {
            AddItem(elementType, amount);
            
            _oneMoreList.Add(
                new InventoryElement { typeName = elementType, Amount = ItemsCache[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void IncreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                if (!ItemsCache.TryGetValue(element.typeName, out var currentAmount))
                {
                    throw new Exception(
                        $"Not found {element.typeName} in warehouse cache. Check that element added to EResource or EFood");
                }

                Debug.LogWarning($"AddResource {element.typeName} {element.Amount}. Before {currentAmount}");

                ItemsCache[element.typeName] += element.Amount;
                _oneMoreList.Add(new InventoryElement
                    { typeName = element.typeName, Amount = ItemsCache[element.typeName] });
            }

            InventoryChanged(_oneMoreList);
        }


        private void InventoryChanged(List<InventoryElement> elements)
        {
            _tempList.Clear();
            foreach (var element in elements)
            {
                _tempList.Add(element);
            }

            _oneMoreList.Clear();

            OnInventoryChanged.Value = _tempList;
            OnInventoryChanged.ForceNotify();
        }

        public void DecreaseResource(string elementType, int amount)
        {
            Debug.LogWarning($"AddResource {elementType} {amount}. Before {ItemsCache[elementType]}");

            ItemsCache[elementType] += amount;
            Debug.LogWarning($"After {ItemsCache[elementType]}");
            _oneMoreList.Add(
                new InventoryElement { typeName = elementType, Amount = ItemsCache[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void DecreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning(
                    $"AddResource {element.typeName} {element.Amount}. Before {ItemsCache[element.typeName]}");
                ItemsCache[element.typeName] += element.Amount;
                _oneMoreList.Add(new InventoryElement
                    { typeName = element.typeName, Amount = ItemsCache[element.typeName] });
            }

            InventoryChanged(_oneMoreList);
        }

        public bool HasEnoughResource(List<InventoryElement> inventoryElements)
        {
            return inventoryElements.All(element => ItemsCache[element.typeName] >= element.Amount);
        }

        public bool HasEnoughResource(string objResourceType, int amount)
        {
            return ItemsCache[objResourceType] >= amount;
        }

        public void SetInitializedInventory(Dictionary<string, int> initItems)
        {
            ItemsCache = initItems;

            var list = new List<InventoryElement>();
            foreach (var item in initItems)
            {
                list.Add(new InventoryElement { typeName = item.Key, Amount = item.Value });
            }

            InventoryChanged(list);
        }

        public Dictionary<string, int> GetInventoryData() => ItemsCache;
    }
}
