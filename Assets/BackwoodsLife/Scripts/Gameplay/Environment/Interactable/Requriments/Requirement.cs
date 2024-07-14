using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Enums;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments
{
    [Serializable]
    public struct Requirement
    {
        public List<RequiredResource> resource;
        public List<RequiredTool> tool;
        public List<RequiredSkill> skill;
        public List<RequiredBuilding> building;
    }

    public interface IRequirement
    {
    }

    [Serializable]
    public struct RequiredResource : IRequirement
    {
        public EResource type;
        public int amount;
    }

    [Serializable]
    public struct RequiredBuilding : IRequirement
    {
        public EBuilding type;
        public int minLevel;
    }


    [Serializable]
    public struct RequiredSkill : IRequirement
    {
        public ESkill type;
        public int minLevel;
    }


    [Serializable]
    public struct RequiredTool : IRequirement
    {
        public ETool type;
        public int minLevel;
    }
}
