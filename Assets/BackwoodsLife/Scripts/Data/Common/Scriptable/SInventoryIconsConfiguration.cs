using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Helpers;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable
{
    [CreateAssetMenu(fileName = "IconsConfiguration",
        menuName = "Backwoods Life Scripts/Inventory/Icons main configuration", order = 1)]
    public class SInventoryIconsConfiguration : ScriptableObject
    {
        public List<ElementIcon> elementsIcons;


        private void OnValidate()
        {
            var List = EnumTypesHelper.GetNamesForInventory();
            foreach (var elementsIcon in elementsIcons)
            {
                Debug.LogWarning(List.Contains(elementsIcon.typeName) + " " + elementsIcon.typeName);
            }
        }
    }

    [Serializable]
    public struct ElementIcon
    {
        public string typeName;
        public Sprite icon;
    }
}
