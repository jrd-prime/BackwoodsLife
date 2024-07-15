using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ReturnCollectables
    {
        public List<CustomReturnCollectable<EResource>> returnResorces;
        public List<CustomReturnCollectable<EFood>> returnFood;
    }
}
