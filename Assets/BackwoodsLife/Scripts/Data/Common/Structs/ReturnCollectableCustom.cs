using System;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct ReturnCollectableCustom<T> where T : Enum
    {
        public T collectableType;
        public CollectRange collectRange;
    }
}
