﻿using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct RequiredItem
    {
        public SGameItemConfig Item;
        public int value;
    }
}
