using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Structs;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data.Inventory
{
    public struct InventoryElement
    {
        public string typeName;
        public int Amount;
    }

    [Serializable]
    public struct CollectableElement
    {
        public string Name;
        public CollectRange Range;
    }

    public class InventoryModel : IInitializable
    {
        private Dictionary<string, int> _inventory = new();
        public ReactiveProperty<List<InventoryElement>> OnInventoryChanged = new();
        private List<InventoryElement> _tempList = new();
        private List<InventoryElement> _oneMoreList = new();

        private Dictionary<string, int> _warehouseItems = new();


        public void Initialize()
        {
            // TODO think about it

            _oneMoreList.Clear();

            foreach (var item in _inventory)
            {
                _oneMoreList.Add(new InventoryElement { typeName = item.Key, Amount = item.Value });
                Debug.LogWarning($"Add {item.Key} {item.Value}");
            }


            InventoryChanged(_oneMoreList);
        }

        public void IncreaseResource(string elementType, int amount)
        {
            Debug.LogWarning($"AddResource {elementType} {amount}. Before {_inventory[elementType]}");

            _inventory[elementType] += amount;
            Debug.LogWarning($"After {_inventory[elementType]}");
            _oneMoreList.Add(new InventoryElement { typeName = elementType, Amount = _inventory[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void IncreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning($"AddResource {element.typeName} {element.Amount}. Before {_inventory[element.typeName]}");
                _inventory[element.typeName] += element.Amount;
                _oneMoreList.Add(new InventoryElement { typeName = element.typeName, Amount = _inventory[element.typeName] });
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
            Debug.LogWarning($"AddResource {elementType} {amount}. Before {_inventory[elementType]}");

            _inventory[elementType] += amount;
            Debug.LogWarning($"After {_inventory[elementType]}");
            _oneMoreList.Add(new InventoryElement { typeName = elementType, Amount = _inventory[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void DecreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning($"AddResource {element.typeName} {element.Amount}. Before {_inventory[element.typeName]}");
                _inventory[element.typeName] += element.Amount;
                _oneMoreList.Add(new InventoryElement { typeName = element.typeName, Amount = _inventory[element.typeName] });
            }

            InventoryChanged(_oneMoreList);
        }

        public bool HasEnoughResource(List<InventoryElement> inventoryElements)
        {
            return inventoryElements.All(element => _inventory[element.typeName] >= element.Amount);
        }

        public bool HasEnoughResource(string objResourceType, int amount)
        {
            return _inventory[objResourceType] >= amount;
        }

        public void SetInitializedInventory(Dictionary<string, int> initItems)
        {
            _inventory = initItems;

            var list = new List<InventoryElement>();
            foreach (var item in initItems)
            {
                list.Add(new InventoryElement { typeName = item.Key, Amount = item.Value });
            }

            InventoryChanged(list);
        }

        public Dictionary<string, int> GetInventoryData() => _inventory;
    }
}
