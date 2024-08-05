using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ReturnCollectable
    {
        public SGameItemConfig item;
        public CollectRange range;
    }
}
