using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    public class CollectSystem : IInteractableSystem
    {
        private WarehouseManager _inventoryManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"inventoryManager:{warehouseManager}");
            _inventoryManager = warehouseManager;
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
