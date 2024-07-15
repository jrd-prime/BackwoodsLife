using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequirementForCollect
    {
        public List<CustomRequirement<ETool>> tool;
        public List<CustomRequirement<ESkill>> skill;
        public List<CustomRequirement<EBuilding>> building;
    }

    [Serializable]
    public struct RequirementForUse
    {
        public List<CustomRequirement<ETool>> tool;
        public List<CustomRequirement<ESkill>> skill;
        public List<CustomRequirement<EBuilding>> building;
    }

    [Serializable]
    public struct RequirementForUpgrade
    {
        public List<CustomRequirement<ETool>> tool;
        public List<CustomRequirement<ESkill>> skill;
        public List<CustomRequirement<EBuilding>> building;
    }

    [Serializable]
    public struct RequirementForUseAndUpgrade
    {
        public List<CustomRequirement<ETool>> tool;
        public List<CustomRequirement<ESkill>> skill;
        public List<CustomRequirement<EBuilding>> building;
    }

    [Serializable]
    public struct CustomRequirement<T> where T : Enum
    {
        public T typeName;
        public int value;
    }

    public interface IRequirement
    {
    }
}
