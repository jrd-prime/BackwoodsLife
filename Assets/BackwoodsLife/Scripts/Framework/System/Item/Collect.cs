using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.System.Item
{
    public sealed class Collect : ItemSystem, IItemSystem
    {
        public bool Process(List<ItemData> itemsData)
        {
            return WarehouseManager.Increase(in itemsData);
        }
    }
}
