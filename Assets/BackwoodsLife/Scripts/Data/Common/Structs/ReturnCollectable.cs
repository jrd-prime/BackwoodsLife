using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.jnew;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ReturnCollectable
    {
        public SCollectableItem item;
        public CollectRange range;
    }
}
