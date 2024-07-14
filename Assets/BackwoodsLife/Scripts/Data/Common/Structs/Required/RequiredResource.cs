using System;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
    [Serializable]
    public struct RequiredResource : IRequirement
    {
        public EResource type;
        public int amount;
    }
}
