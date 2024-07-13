using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data.Inventory
{
    public struct InventoryElement
    {
        public Enum Type;
        public int Amount;
    }

    public class InventoryModel : IInitializable
    {
        private Dictionary<Enum, Int32> _inventory = new();
        public ReactiveProperty<List<InventoryElement>> OnInventoryChanged = new();
        private List<InventoryElement> _tempList = new();
        private List<InventoryElement> _oneMoreList = new();

        public void Initialize()
        {
            foreach (Enum t in Enum.GetValues(typeof(EResourceType)))
            {
                if (t is EResourceType.None) continue;

                _inventory.Add(t, RandomCollector.GetRandom(0, 5));
            }

            // TODO load and initialize inventory data
            // TODO think about it

            _oneMoreList.Clear();

            foreach (var item in _inventory)
            {
                _oneMoreList.Add(new InventoryElement { Type = item.Key, Amount = item.Value });
                Debug.LogWarning($"Add {item.Key} {item.Value}");
            }


            InventoryChanged(_oneMoreList);
        }

        public void IncreaseResource(Enum elementType, int amount)
        {
            Debug.LogWarning($"AddResource {elementType} {amount}. Before {_inventory[elementType]}");

            _inventory[elementType] += amount;
            Debug.LogWarning($"After {_inventory[elementType]}");
            _oneMoreList.Add(new InventoryElement { Type = elementType, Amount = _inventory[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void IncreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning($"AddResource {element.Type} {element.Amount}. Before {_inventory[element.Type]}");
                _inventory[element.Type] += element.Amount;
                _oneMoreList.Add(new InventoryElement { Type = element.Type, Amount = _inventory[element.Type] });
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

        public void DecreaseResource(Enum elementType, int amount)
        {
            Debug.LogWarning($"AddResource {elementType} {amount}. Before {_inventory[elementType]}");

            _inventory[elementType] += amount;
            Debug.LogWarning($"After {_inventory[elementType]}");
            _oneMoreList.Add(new InventoryElement { Type = elementType, Amount = _inventory[elementType] });
            InventoryChanged(_oneMoreList);
        }

        public void DecreaseResource(List<InventoryElement> elements)
        {
            foreach (var element in elements)
            {
                Debug.LogWarning($"AddResource {element.Type} {element.Amount}. Before {_inventory[element.Type]}");
                _inventory[element.Type] += element.Amount;
                _oneMoreList.Add(new InventoryElement { Type = element.Type, Amount = _inventory[element.Type] });
            }

            InventoryChanged(_oneMoreList);
        }

        public bool HasEnoughResource(List<InventoryElement> inventoryElements)
        {
            return inventoryElements.All(element => _inventory[element.Type] >= element.Amount);
        }

        public bool HasEnoughResource(Enum objResourceType, int amount)
        {
            return _inventory[objResourceType] >= amount;
        }
    }
}
