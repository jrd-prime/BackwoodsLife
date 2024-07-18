using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequiredSkill : IRequirement
    {
        public ESkill type;
        public int minLevel;
    }
}
