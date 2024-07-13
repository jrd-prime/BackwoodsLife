using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments
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
