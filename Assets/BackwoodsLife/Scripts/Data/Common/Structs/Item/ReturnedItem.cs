using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct ReturnedItem
    {
        public SGameItemConfig Item;
        public CollectRange Range;
    }
}
