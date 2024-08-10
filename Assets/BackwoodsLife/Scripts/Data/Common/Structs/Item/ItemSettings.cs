﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct CraftItemSetting<T> where T : SGameItemConfig
    {
        public ECraftingType craftingType;
        [Range(30, 10800)] public int craftingTime; // 30sec => 3h
        public List<ItemDataWithConfig<T>> recipeForOneItem;
    }

    [Serializable]
    public struct ItemDataWithConfigAndRange
    {
        public SGameItemConfig item;
        public CollectRange range;
    }

    [Serializable]
    public struct ItemDataWithConfig
    {
        public SItemConfig item;
        public int quantity;
    }

    [Serializable]
    public struct ItemDataWithConfigAndActual
    {
        public SItemConfig item;
        public int actual;
        public int required;
    }

    [Serializable]
    public struct ItemDataWithConfigAndRange<T> where T : SGameItemConfig
    {
        public T item;
        public CollectRange range;
    }

    [Serializable]
    public struct ItemDataWithConfig<T> where T : SGameItemConfig
    {
        public T item;
        public int count;
    }
}
