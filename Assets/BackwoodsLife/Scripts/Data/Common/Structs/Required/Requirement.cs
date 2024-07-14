using System;
using System.Collections.Generic;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
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
}
