using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    public class SpendSystem
    {
        private WarehouseManager _inventoryManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"inventoryManager:{warehouseManager}");
            _inventoryManager = warehouseManager;
        }
    }
}
