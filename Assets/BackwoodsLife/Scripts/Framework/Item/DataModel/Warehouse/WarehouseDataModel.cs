using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse
{
    public class WarehouseItemDataModel : ItemDataRepository
    {
        public override void Initialize()
        {
            List<Type> list = new() { typeof(ResourceType), typeof(FoodType) };

            // Init to zero
            foreach (var name in list.SelectMany(Enum.GetNames))
                ItemsCache.TryAdd(name, 0);

            // TODO think about it

            TempListForDataChanges.Clear();

            foreach (var item in ItemsCache)
            {
                // Debug.LogWarning($"Add {item.Key} {item.Value}");
                TempListForDataChanges.Add(new ItemDataChanged { Name = item.Key, From = 0, To = item.Value });
            }

            RepositoryDataChanged();
        }
    }
}
