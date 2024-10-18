using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe
{
    [CreateAssetMenu(
        fileName = "NewSimpleRecipe",
        menuName = SOPathName.RecipeItemPath + "Simple Recipe",
        order = 1)]
    public class SRecipeSimple : SRecipe
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            productionLevelType = ProductionLevelType.Simple;
        }
    }
}
