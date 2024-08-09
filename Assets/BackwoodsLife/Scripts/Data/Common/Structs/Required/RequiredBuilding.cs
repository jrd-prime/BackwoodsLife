using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Required
{
   
    [Serializable]
    public struct RequiredBuilding : IRequirement
    {
        public EUseAndUpgradeName type;
        public int minLevel;
    }
}
