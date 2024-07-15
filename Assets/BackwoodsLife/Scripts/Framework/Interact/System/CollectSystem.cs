using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
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

        public void Collect(ref List<InventoryElement> obj)
        {
            Debug.LogWarning("COLLECT");
            Assert.IsNotNull(_inventoryManager, "inventoryManager is null");
            Assert.IsNotNull(obj, "obj is null");
            _inventoryManager.IncreaseResource(obj);
        }
    }
}
