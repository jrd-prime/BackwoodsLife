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
            Debug.LogWarning("Collect ADD");
            var data = obj.collectableData;
            var amount = RandomCollector.GetRandom(data.collectRange.min, data.collectRange.max);
            _inventoryManager.IncreaseResource(obj.resourceType, amount);
            _inventoryManager.IncreaseResource(new List<InventoryElement>
            {
                new() { Type = obj.resourceType, Amount = amount },
                new() { Type = EResourceType.Wood, Amount = amount * 2 }
            });

            Debug.LogWarning("Collect REMOVE");
            _inventoryManager.DecreaseResource(obj.resourceType, amount);

            _inventoryManager.DecreaseResources(new List<InventoryElement>
            {
                new() { Type = obj.resourceType, Amount = 33 },
                new() { Type = EResourceType.Wood, Amount = 22 }
            });
        }
    }
}
