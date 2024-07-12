using System;
using System.Collections;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory.JObjects;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.ToolObjects;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.Requriments
{
    [Serializable]
    public struct Requirement
    {
        [FormerlySerializedAs("requiredTool")] public List<RequiredTool> tool;

        [FormerlySerializedAs("requiredSkill")]
        public List<RequiredSkill> skill;

        [FormerlySerializedAs("requiredBuilding")]
        public List<RequiredBuilding> building;
    }

    public interface IRequirement
    {
    }

    [Serializable]
    public struct RequiredBuilding : IRequirement
    {
        public EBuildingType Type;
        public int MinLevel;
    }


    [Serializable]
    public struct RequiredSkill : IRequirement
    {
        public ESkillType Type;
        public int MinLevel;
    }


    [Serializable]
    public struct RequiredTool : IRequirement
    {
        public EToolType Type;
        public int MinLevel;
    }
}
