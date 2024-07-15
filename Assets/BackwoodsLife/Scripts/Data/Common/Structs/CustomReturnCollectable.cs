using System;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct CustomReturnCollectable<T> where T : Enum
    {
        public T type;
        public CollectRange range;
    }
}
