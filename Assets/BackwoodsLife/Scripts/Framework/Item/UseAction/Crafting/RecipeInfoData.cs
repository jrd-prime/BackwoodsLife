using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public struct RecipeInfoData
    {
        public string Title;
        public string Description;
        public Sprite Icon;
        public List<ItemDataWithConfig<SGameItemConfig>> Ingredients;
    }
}
