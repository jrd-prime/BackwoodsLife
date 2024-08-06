using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "FoodItem",
        menuName = SOPathName.GameItemPath + "Food Item",
        order = 1)]
    public class SFoodItem : SCraftableItem<SFoodItem>
    {
        [Title("Food Item Config")] public EFood foodType;

        protected override void OnValidate()
        {
            base.OnValidate();

            Assert.IsTrue(
                foodType.ToString() == itemName,
                "Check item name and food type. Game item config: " + name);
        }

        private void Awake()
        {
            gameItemType = EGameItemType.Food;
        }
    }
}
