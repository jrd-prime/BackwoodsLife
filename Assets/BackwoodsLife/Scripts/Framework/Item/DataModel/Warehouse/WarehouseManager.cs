using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse
{
    /// <summary>
    /// Responsible for the repository as a whole. Has access to the model. Responsible for changing data in the model.
    /// </summary>
    public class WarehouseManager : CustomItemDataRepository<WarehouseItemDataModel>
    {
        protected override Dictionary<string, int> GetOnLoadItemsAndValues()
        {
            return (from type in new List<Type> { typeof(ResourceType), typeof(FoodType) }
                    from name in Enum.GetNames(type)
                    where name != "None"
                    select name)
                .ToDictionary(name => name, _ => 0);
        }
    }
}
