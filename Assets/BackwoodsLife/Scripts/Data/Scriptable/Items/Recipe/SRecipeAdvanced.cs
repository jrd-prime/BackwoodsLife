using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe
{
    [CreateAssetMenu(
        fileName = "NewAdvancedRecipe",
        menuName = SOPathName.RecipeItemPath + "Advanced Recipe",
        order = 1)]
    public class SRecipeAdvanced : SRecipe
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            productionLevel = EProductionLevel.Advanced;
        }
    }
}
