﻿using System;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Systems
{
    public class CollectSystem : IInteractableSystem
    {
        private InventoryManager _inventoryManager;

        [Inject]
        private void Construct(InventoryManager inventoryManager)
        {
            Debug.Log($"inventoryManager:{inventoryManager}");
            _inventoryManager = inventoryManager;
        }

        public void Collect(ref SCollectable obj)
        {
            Debug.LogWarning("COLLECT");
            var data = obj.returnedCollectables;

            switch (data.Count)
            {
                case 1:
                {
                    // var amount = RandomCollector.GetRandom(data[0].collectRange.min, data[0].collectRange.max);
                    // _inventoryManager.IncreaseResource(data[0].resourceType.ToString(), amount);
                    break;
                }
                case > 1:

                    // var newList = (
                    // from item in data
                    // let amount = RandomCollector.GetRandom(item.collectRange.min, item.collectRange.max)
                    // select new InventoryElement { Type = item.resourceType.ToString(), Amount = amount })
                    // .ToList();

                    // _inventoryManager.IncreaseResource(newList);
                    break;
                default:
                    throw new Exception("Incorrect collectable data. " + obj.name);
            }
        }
    }
}
