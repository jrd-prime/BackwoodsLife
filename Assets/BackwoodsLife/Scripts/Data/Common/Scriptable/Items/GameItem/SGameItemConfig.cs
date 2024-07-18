using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
{
    public abstract class SGameItemConfig : SItemConfig
    {
        [ReadOnly] public EGameItem gameItemType;
    }
}
