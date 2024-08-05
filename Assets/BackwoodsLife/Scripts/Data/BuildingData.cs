using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Framework.Helpers;

namespace BackwoodsLife.Scripts.Data
{
    public class BuildingData : ItemDataHolder
    {
        public override void Initialize()
        {
            ItemsCache = new Dictionary<string, int>();

            // TODO load saved data and initialize

            // List of enums that can be stored in the warehouse
            List<Type> list = new() { typeof(EBuilding) };

            // Init to zero
            foreach (var name in list.SelectMany(Enum.GetNames))
                ItemsCache.TryAdd(name,0);

            // foreach (var keyValuePair in ItemsCache)
            // {
            //     Debug.LogWarning($"{keyValuePair.Key} {keyValuePair.Value}");
            // }
        }
    }
}
