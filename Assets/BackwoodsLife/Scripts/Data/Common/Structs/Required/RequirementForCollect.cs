using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Skill;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Tool;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse.Resource;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem.Building;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequirementForCollect
    {
        public List<CustomRequirement<SToolItem>> tool;
        public List<CustomRequirement<SSkillItem>> skill;
        public List<CustomRequirement<SBuildingItem>> building;
    }

    // [Serializable]
    // public struct RequirementForUse
    // {
    //     public List<CustomRequirement<ETool>> tool;
    //     public List<CustomRequirement<ESkill>> skill;
    //     public List<CustomRequirement<EBuilding>> building;
    // }

    [Serializable]
    public struct RequirementForUpgrade
    {
        public List<CustomRequirement<SResourceItem>> resource;
        public List<CustomRequirement<SToolItem>> tool;
        public List<CustomRequirement<SSkillItem>> skill;
        public List<CustomRequirement<SBuildingItem>> building;
    }

    // [Serializable]
    // public struct RequirementForUseAndUpgrade
    // {
    //     public List<CustomRequirement<ETool>> tool;
    //     public List<CustomRequirement<ESkill>> skill;
    //     public List<CustomRequirement<EBuilding>> building;
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
