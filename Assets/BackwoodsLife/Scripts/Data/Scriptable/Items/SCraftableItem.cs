using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using Sirenix.OdinInspector;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    public abstract class SCraftableItem<T> : SGameItemConfig where T : SGameItemConfig
    {
        [Title("Craftable")] public bool craftable;
        [ShowIf("@craftable")] public CraftItemSetting<T> craftItemSetting;
    }
}
