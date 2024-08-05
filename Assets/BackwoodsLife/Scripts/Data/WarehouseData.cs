using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Helpers;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data
{
    public class WarehouseData : ItemDataHolder
    {
        public override void Initialize()
        {
            // Debug.LogWarning("Warehouse data init");
            ItemsCache = new Dictionary<string, int>();

            // TODO load saved data and initialize

            // List of enums that can be stored in the warehouse
            List<Type> list = new() { typeof(EResource), typeof(EFood) };

            // Init to zero
            foreach (var name in list.SelectMany(Enum.GetNames))
                ItemsCache.TryAdd(name, 0);

            // foreach (var keyValuePair in ItemsCache)
            // {
            //     Debug.LogWarning($"{keyValuePair.Key} {keyValuePair.Value}");
            // }
        }

    }
}
