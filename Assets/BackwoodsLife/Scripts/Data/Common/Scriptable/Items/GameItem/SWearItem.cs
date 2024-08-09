using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "InventoryItem",
        menuName = SOPathName.GameItemPath + "Inventory Item",
        order = 1)]
    public class SWearItem : SCraftableItem<SWearItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();

        }

        private void Awake()
        {
            gameItemType = EGameItemType.Wear;
        }
    }
}
