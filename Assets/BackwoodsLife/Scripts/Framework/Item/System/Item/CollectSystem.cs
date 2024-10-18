using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.Item.System.Item
{
    public sealed class CollectSystem : ItemSystem, IItemSystem
    {
        public bool Process(List<ItemDto> itemsData)
        {
            return WarehouseManager.Increase(in itemsData);
        }
    }
}
