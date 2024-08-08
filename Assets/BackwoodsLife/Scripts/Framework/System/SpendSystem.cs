using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Framework.Module.ItemsData;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System
{
    public class SpendSystem : IInteractableSystem
    {
        private WarehouseManager _warehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"inventoryManager:{warehouseManager}");
            _warehouseManager = warehouseManager;
        }

        public void Spend(List<KeyValuePair<SItemConfig, int>> toList)
        {
        }

        public void Process(IItemDataManager itemDataManager, List<ItemData> itemsWithConfigToCollect,
            Action<List<ItemData>> callback)
        {
            foreach (var item in itemsWithConfigToCollect)
            {
                _warehouseManager.DecreaseResource(item.Name, item.Quantity);
            }
        }
    }
}
