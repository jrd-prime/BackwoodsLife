using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Wear
{
    [CreateAssetMenu(
        fileName = "InventoryItem",
        menuName = SOPathName.GameItemPath + "Inventory Item",
        order = 1)]
    public class SWearItem : SGameItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();

            gameItemType = EGameItem.Wear;
        }
    }
}
