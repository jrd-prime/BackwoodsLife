using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ReturnCollectables
    {
        public List<ReturnCollectableCustom<EResource>> returnResorces;
        public List<ReturnCollectableCustom<EFood>> returnFood;
    }
}
