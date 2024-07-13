using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Framework.Scriptable;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Systems
{
    public class CollectSystem
    {
        private InventoryManager _inventoryManager;

        [Inject]
        private void Construct(InventoryManager inventoryManager)
        {
            Debug.Log($"inventoryManager:{inventoryManager}");
            _inventoryManager = inventoryManager;
        }

        public void Collect(ref SInteractableObjectConfig obj)
        {
            Debug.LogWarning("COLLECT");
            var data = obj.collectableData;
            var amount = RandomCollector.GetRandom(data.collectRange.min, data.collectRange.max);
            _inventoryManager.IncreaseResource(obj.resourceType, amount);
            _inventoryManager.IncreaseResource(new List<InventoryElement>
            {
                new() { Type = obj.resourceType, Amount = amount },
                new() { Type = EResourceType.Wood, Amount = amount * 2 }
            });
        }
    }
}
