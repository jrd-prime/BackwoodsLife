using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Enums;

namespace BackwoodsLife.Scripts.Gameplay.NewLook.Struct
{
    [Serializable]
    public struct ReturnCollectables
    {
        public List<ReturnCollectableCustom<EResource>> returnResorces;
        public List<ReturnCollectableCustom<EFood>> returnFood;
    }
}
