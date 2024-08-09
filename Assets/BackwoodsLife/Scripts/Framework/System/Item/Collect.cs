using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System.Item
{
    public class Collect : IItemSystem
    {
        private WarehouseManager _warehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager) => _warehouseManager = warehouseManager;

        public bool Process(List<ItemData> itemsData)
        {
            Assert.IsNotNull(_warehouseManager, "WarehouseManager is null");
            return _warehouseManager.Increase(in itemsData);
        }
    }
}
