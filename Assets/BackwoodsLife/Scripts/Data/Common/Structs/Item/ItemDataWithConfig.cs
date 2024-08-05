using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct ItemDataWithConfig
    {
        public SGameItemConfig item;
        public CollectRange range;
    }
}
