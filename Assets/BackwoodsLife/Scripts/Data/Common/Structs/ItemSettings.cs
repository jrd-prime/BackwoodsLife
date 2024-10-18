using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ItemCraftSettings<T> where T : GameItemSettings
    {
        [FormerlySerializedAs("craftingType")] public ProductionType productionType;
        [Range(30, 10800)] public int craftingTime; // 30sec => 3h
        public List<ItemDataWithConfig<T>> recipeForOneItem;
    }

    [Serializable]
    public struct ItemDataWithConfigAndRange
    {
        public GameItemSettings item;
        [FormerlySerializedAs("range")] public ItemRangeDto rangeDto;
    }

    [Serializable]
    public struct ItemDataWithConfig
    {
        public ItemSettings item;
        public int quantity;
    }

    [Serializable]
    public struct ItemDataWithConfigAndActual
    {
        public ItemSettings item;
        public int actual;
        public int required;
    }

    [Serializable]
    public struct ItemDataWithConfigAndRange<T> where T : GameItemSettings
    {
        public T item;
        [FormerlySerializedAs("range")] public ItemRangeDto rangeDto;
    }

    [Serializable]
    public struct ItemDataWithConfig<T> where T : GameItemSettings
    {
        public T item;
        public int count;
    }
}
