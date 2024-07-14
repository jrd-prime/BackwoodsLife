using System;

namespace BackwoodsLife.Scripts.Gameplay.NewLook.Struct
{
    [Serializable]
    public struct ReturnCollectableCustom<T> where T : Enum
    {
        public T collectableType;
        public CollectRange collectRange;
    }
}
