using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.System.Item
{
    public sealed class Spend : ItemSystem, IItemSystem
    {
        public bool Process(List<ItemData> itemsData)
        {
            foreach (var item in itemsData)
            {
                WarehouseManager.DecreaseResource(item.Name, item.Quantity);
            }

            return true;
        }
    }
}
