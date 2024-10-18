using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Const;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Scriptable.Items.Recipe;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Settings
{
    [CreateAssetMenu(
        fileName = "MainConfigurations",
        menuName = SOPathName.ConfigPath + "Main Configurations List",
        order = 100)]
    public class SMainConfig : ScriptableObject
    {
        [FormerlySerializedAs("characterConfiguration")] [Title("Character")]
        public SCharacterConfig characterConfig;

        [Title("Items")] public GameItemsList GameItemsList;

        public WorldItemsList WorldItemsList;
        public RecipeItemsList recipeItemsList;

        private string prefix = "MainConfig. Check.";

        private void OnValidate()
        {
            Check(GameItemsList.resourceItems, typeof(ResourceType), "Resource");
            Check(GameItemsList.foodItems, typeof(FoodType), "Food");
            Check(GameItemsList.toolItems, typeof(ToolType), "Tool");
            Check(GameItemsList.skillItems, typeof(SkillType), "Skill");

            Check(WorldItemsList.buildingItems, typeof(UsableAndUpgradableType), "Building");
            Check(WorldItemsList.collectableItems, typeof(CollectableType), "Collectable");
            Check(WorldItemsList.placeItems, typeof(UsableType), "Place");
        }

        public void Check<T>(List<CustomItemConfig<T>> resourceItems, Type type, string id) where T : ItemSettings
        {
            var count = Enum.GetNames(type).Length;

            foreach (var resourceItem in resourceItems)
            {
                if (resourceItem.config == null)
                    throw new NullReferenceException($"{prefix} One or more {id} config is null");
            }

            if (resourceItems.Count != count)
                throw new Exception(
                    $"{prefix} Count of {id}({resourceItems.Count}) in {name} is not equal enum E{id}({count})");

            Debug.Log($"{prefix} {id}: <color=green>OK</color>");
        }
    }

    [Serializable]
    public record RecipeItemsList
    {
        public List<CustomRecipe<SRecipeSimple>> recipeSimpleList;
        public List<CustomRecipe<SRecipeAdvanced>> recipeAdvancedList;
        public List<CustomRecipe<SRecipeExpert>> recipeExpertList;
    }

    [Serializable]
    public struct WorldItemsList
    {
        public List<CustomItemConfig<UseAndUpgradeItem>> buildingItems;
        public List<CustomItemConfig<CollectableItem>> collectableItems;
        public List<CustomItemConfig<UseOnlyItem>> placeItems;
    }

    [Serializable]
    public struct GameItemsList
    {
        public List<CustomItemConfig<ResourceItem>> resourceItems;
        public List<CustomItemConfig<ToolItem>> toolItems;
        public List<CustomItemConfig<SkillItem>> skillItems;
        public List<CustomItemConfig<FoodItem>> foodItems;
    }

    [Serializable]
    public struct CustomItemConfig<T> where T : ItemSettings
    {
        public T config;
    }

    [Serializable]
    public struct CustomRecipe<T> where T : SRecipe
    {
        public T recipe;
    }
}
