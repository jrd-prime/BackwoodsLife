using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System.Item
{
    public class Spend : IItemSystem
    {
        private WarehouseManager _warehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"inventoryManager:{warehouseManager}");
            _warehouseManager = warehouseManager;
        }

        public bool Process(List<ItemData> itemsData)
        {
            foreach (var item in itemsData)
            {
                _warehouseManager.DecreaseResource(item.Name, item.Quantity);
            }

            return true;
        }
    }
}
