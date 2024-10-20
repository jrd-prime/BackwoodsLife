﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe
{
    public class SRecipe : ScriptableObject
    {
        public ProductionType productionType;
        [FormerlySerializedAs("productionLevel")] [ReadOnly] public ProductionLevelType productionLevelType;
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
        public string description;
        public ItemDataWithConfig<GameItemSettings> returnedItem;
        public List<ItemDataWithConfig<GameItemSettings>> ingredients;
    }
}
