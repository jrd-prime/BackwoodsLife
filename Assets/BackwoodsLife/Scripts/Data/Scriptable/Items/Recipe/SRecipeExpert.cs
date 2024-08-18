using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe
{
    [CreateAssetMenu(
        fileName = "NewExpertRecipe",
        menuName = SOPathName.RecipeItemPath + "Expert Recipe",
        order = 1)]
    public class SRecipeExpert : SRecipe
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            productionLevel = EProductionLevel.Expert;
        }
    }
}
