using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Const;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "InventoryItem",
        menuName = SOPathName.GameItemPath + "Inventory Item",
        order = 1)]
    public class WearItem : CraftableItem<WearItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();

        }

        private void Awake()
        {
            gameItemType = GameItemType.Wear;
        }
    }
}
