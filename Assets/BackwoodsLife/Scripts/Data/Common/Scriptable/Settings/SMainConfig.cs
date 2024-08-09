using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
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


        private string prefix = "MainConfig. Check.";

        private void OnValidate()
        {
            Check(GameItemsList.resourceItems, typeof(EResource), "Resource");
            Check(GameItemsList.foodItems, typeof(EFood), "Food");
            Check(GameItemsList.toolItems, typeof(ETool), "Tool");
            Check(GameItemsList.skillItems, typeof(ESkill), "Skill");

            Check(WorldItemsList.buildingItems, typeof(EUseAndUpgradeName), "Building");
            Check(WorldItemsList.collectableItems, typeof(ECollectName), "Collectable");
            Check(WorldItemsList.placeItems, typeof(EUseName), "Place");
        }

        public void Check<T>(List<CustomItemConfig<T>> resourceItems, Type type, string id) where T : SItemConfig
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

            Debug.LogWarning($"{prefix} {id}: <color=green>OK</color>");
        }
    }

    [Serializable]
    public struct WorldItemsList
    {
        public List<CustomItemConfig<SUseAndUpgradeItem>> buildingItems;
        public List<CustomItemConfig<SCollectableItem>> collectableItems;
        public List<CustomItemConfig<SUseOnlyItem>> placeItems;
    }

    [Serializable]
    public struct GameItemsList
    {
        public List<CustomItemConfig<SResourceItem>> resourceItems;
        public List<CustomItemConfig<SToolItem>> toolItems;
        public List<CustomItemConfig<SSkillItem>> skillItems;
        public List<CustomItemConfig<SFoodItem>> foodItems;
    }

    [Serializable]
    public struct CustomItemConfig<T> where T : SItemConfig
    {
        public T config;
    }
}
