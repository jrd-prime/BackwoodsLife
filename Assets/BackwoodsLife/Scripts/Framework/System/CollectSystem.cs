﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System
{
    /// <summary>
    /// Collect items, xp , etc. And update items amount from some bonuses
    /// </summary>
    public class CollectSystem : IInteractableSystem
    {
        private WarehouseManager _warehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager)
        {
            Debug.Log($"Inject CollectSystem");
            _warehouseManager = warehouseManager;
        }

        public void Collect(List<ItemDataWithConfig> itemsWithConfigToCollect,
            Action<List<ItemData>> callback)
        {
            Debug.LogWarning("COLLECT");
            Assert.IsNotNull(_warehouseManager, "WarehouseManager is null");
            Assert.IsNotNull(itemsWithConfigToCollect, "itemsToCollect is null");

            // TODO check bonuses from tool, skill, etc.

            var processedItems = new List<ItemData>();

            foreach (var item in itemsWithConfigToCollect)
            {
                var itemAmount = RandomCollector.GetRandom(item.range.min, item.range.max);

                processedItems.Add(new ItemData { Name = item.item.itemName, Quantity = itemAmount });
            }

            callback?.Invoke(processedItems);
            _warehouseManager.Increase(in processedItems);
        }
    }
}
