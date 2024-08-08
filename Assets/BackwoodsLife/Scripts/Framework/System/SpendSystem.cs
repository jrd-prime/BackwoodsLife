using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System
{
    public class SpendSystem : IInteractableSystem
    {
        private WarehouseManager _inventoryManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"inventoryManager:{warehouseManager}");
            _inventoryManager = warehouseManager;
        }


        public void Spend(List<KeyValuePair<SItemConfig, int>> toList)
        {
            foreach (var (key, value) in toList)
            {
                _inventoryManager.DecreaseResource(key.itemName, value);
            }
        }
    }
}
