using System;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
   
    [Serializable]
    public struct RequiredBuilding : IRequirement
    {
        public EBuilding type;
        public int minLevel;
    }
}
