using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Module.ItemsData;
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
        private IItemDataManager _currentManager;

        // [Inject]
        // private void Construct(WarehouseManager warehouseManager)
        // {
        //     Debug.Log($"Inject CollectSystem");
        //     _warehouseManager = warehouseManager;
        // }

        public void Process(IItemDataManager itemDataManager, List<ItemData> itemsWithConfigToCollect,
            Action<List<ItemData>> callback)
        {
            Assert.IsNotNull(itemDataManager, "IItemDataManager is null");
            Assert.IsNotNull(itemsWithConfigToCollect, "List<ItemDataWithConfig> is null");
            Assert.IsNotNull(callback, "Action<List<ItemData>> is null");

            _currentManager = itemDataManager;

            Debug.LogWarning($"Collect system. {itemDataManager}");

            // TODO check bonuses from tool, skill, etc.

            var processedItems = new List<ItemData>();

            foreach (var item in itemsWithConfigToCollect)
            {
                // var itemAmount = RandomCollector.GetRandom(item.range.min, item.range.max);

                // processedItems.Add(new ItemData { Name = item.item.itemName, Quantity = itemAmount });
            }

            callback.Invoke(processedItems);
            _currentManager.Increase(in processedItems);
        }
    }
}
