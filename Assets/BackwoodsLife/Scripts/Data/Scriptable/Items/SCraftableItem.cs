using System;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using Sirenix.OdinInspector;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    public abstract class SCraftableItem<T> : SGameItemConfig where T : SGameItemConfig
    {
        [Title("Craftable")] public bool craftable;
        [ShowIf("@craftable")] public CraftItemSetting<T> craftItemSetting;
        
        [Title("Can buy")] public bool canBuy;
        [ShowIf("@canBuy")] public BuyItemSetting buySetting;
    }

    [Serializable]
    public struct BuyItemSetting
    {
        public int price;
    }
}
