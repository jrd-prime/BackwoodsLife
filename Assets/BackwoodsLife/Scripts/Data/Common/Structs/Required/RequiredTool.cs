using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequiredTool : IRequirement
    {
        public ETool type;
        public int minLevel;
    }
}
