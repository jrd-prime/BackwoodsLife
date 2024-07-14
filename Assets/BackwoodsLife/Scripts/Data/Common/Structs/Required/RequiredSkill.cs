using System;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequiredSkill : IRequirement
    {
        public ESkill type;
        public int minLevel;
    }
}
