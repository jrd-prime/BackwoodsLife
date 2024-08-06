using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequirementForCollect
    {
        public List<CustomRequirement<SToolItem>> tool;
        public List<CustomRequirement<SSkillItem>> skill;
        public List<CustomRequirement<SUseAndUpgradeItem>> building;
    }

    [Serializable]
    public struct RequirementForUpgrade
    {
        public List<CustomRequirement<SResourceItem>> resource;
        public List<CustomRequirement<SToolItem>> tool;
        public List<CustomRequirement<SSkillItem>> skill;
        public List<CustomRequirement<SUseAndUpgradeItem>> building;
    }

    [Serializable]
    public struct CustomRequirement<T> where T : SItemConfig
    {
        public T typeName;
        [Range(0, 99)] public int value;
    }

    public interface IRequirement
    {
    }
}
