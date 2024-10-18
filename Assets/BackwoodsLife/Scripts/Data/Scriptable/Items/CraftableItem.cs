using System;
using BackwoodsLife.Scripts.Data.Common.Structs;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    public abstract class CraftableItem<T> : GameItemSettings where T : GameItemSettings
    {
        [Title("Craftable")] public bool craftable;
        [FormerlySerializedAs("craftItemSetting")] [ShowIf("@craftable")] public ItemCraftSettings<T> itemCraftSettings;
        
        [Title("Can buy")] public bool canBuy;
        [ShowIf("@canBuy")] public BuyItemSetting buySetting;
    }

    [Serializable]
    public struct BuyItemSetting
    {
        public int price;
    }
}
