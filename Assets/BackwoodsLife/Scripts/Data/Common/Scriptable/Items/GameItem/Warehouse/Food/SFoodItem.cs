using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse.Food
{
    [CreateAssetMenu(
        fileName = "FoodItem",
        menuName = SOPathName.GameItemPath + "Food Item",
        order = 1)]
    public class SFoodItem : SWarehouseItem
    {
        protected override void OnValidate()
        {
            base.OnValidate();

            gameItemType = EGameItem.Food;
        }
    }
}
