using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe
{
    public class SRecipe : ScriptableObject
    {
        public EProductionType productionType;
        [ReadOnly] public EProductionLevel productionLevel;
        public ItemRecipeData recipeData;

        protected virtual void OnValidate()
        {
            Assert.IsNotNull(recipeData.returnedItem.item, "Returned item is null here: " + name);
            name = recipeData.returnedItem.item.itemName;
            Assert.IsNotNull(recipeData.ingredients, "Ingredients is null here: " + name);
        }
    }

    [Serializable]
    public struct ItemRecipeData
    {
        public ItemDataWithConfig<SGameItemConfig> returnedItem;
        public List<ItemDataWithConfig<SGameItemConfig>> ingredients;
    }
}
