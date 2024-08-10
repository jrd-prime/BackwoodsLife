﻿using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData.Building
{
    public class BuildingsDataModel : DontKnowRepository
    {
        public override void Initialize()
        {
            ItemsCache = new Dictionary<string, int>();

            // TODO load saved data and initialize
            List<Type> list = new() { typeof(EUseAndUpgradeName) };

            // Init to zero
            foreach (var name in list.SelectMany(Enum.GetNames))
                ItemsCache.TryAdd(name, 0);
        }
    }
}
