using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct RequiredForCollectDto
    {
        public List<RequiredItemSettings<ToolItem>> tool;
        public List<RequiredItemSettings<SkillItem>> skill;
        public List<RequiredItemSettings<UseAndUpgradeItem>> building;
    }

    [Serializable]
    public struct RequiredForUpgradeDto
    {
        public List<RequiredItemSettings<ResourceItem>> resource;
        public List<RequiredItemSettings<ToolItem>> tool;
        public List<RequiredItemSettings<SkillItem>> skill;
        public List<RequiredItemSettings<UseAndUpgradeItem>> building;
    }

    [Serializable]
    public struct RequiredItemSettings<T> where T : ItemSettings
    {
        [FormerlySerializedAs("typeName")] public T itemSettings;
        [Range(0, 99)] public int value;
    }
}
