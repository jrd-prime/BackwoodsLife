using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(
        fileName = "MainConfigurations",
        menuName = SOPathName.ConfigPath + "Main Configurations List",
        order = 100)]
    public class SMainConfigurationsList : ScriptableObject
    {
        [Title("Character")] public SCharacterConfiguration characterConfiguration;

        [Title("Items")] public GameItemsList GameItemsList;

        public WorldItemsList WorldItemsList;
    }

    [Serializable]
    public struct WorldItemsList
    {
        public List<Blabla<SBuildingItem>> buildingItems;
        public List<Blabla<SCollectableItem>> collectableItems;
        public List<Blabla<SPlaceItem>> warehouseItems;
        
    }

    [Serializable]
    public struct GameItemsList
    {
        public List<Blabla<SResourceItem>> resourceItems;
        public List<Blabla<SToolItem>> toolItems;
        public List<Blabla<SSkillItem>> skillItems;
        public List<Blabla<SFoodItem>> foodItems;
    }

    [Serializable]
    public struct Blabla<T> where T : SItemConfig
    {
        public T Config;
    }
}
