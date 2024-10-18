using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "FoodItem",
        menuName = SOPathName.GameItemPath + "Food Item",
        order = 1)]
    public class FoodItem : CraftableItem<FoodItem>
    {
        [FormerlySerializedAs("foodType")] [Title("Food Item Config")] public FoodType foodTypeType;

        protected override void OnValidate()
        {
            base.OnValidate();

            Assert.IsTrue(
                foodTypeType.ToString() == itemName,
                "Check item name and food type. Game item config: " + name);
        }

        private void Awake()
        {
            gameItemType = GameItemType.Food;
        }
    }
}
