﻿using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel
{
    public interface IItemDataManager : IInitializable
    {
        public bool Increase(string res, int amount);
        public bool Increase(in List<ItemDto> inventoryElements);
        public bool DecreaseResource(string objResourceType, int amount);
        public bool DecreaseResource(in List<ItemDto> inventoryElements);
    }
}
