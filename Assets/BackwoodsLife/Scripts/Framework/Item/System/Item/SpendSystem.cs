using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.Item.System.Item
{
    public sealed class SpendSystem : ItemSystem, IItemSystem
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
