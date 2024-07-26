using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(fileName = "WorldItemsList", menuName = SOPathName.ItemsConfigPath + "World Items list config",
        order = 1)]
    public class SWorldItemsList : SItemsConfigList<SWorldItemConfig>
    {
        public List<SWorldItemConfig> worldItems;

        private void OnValidate()
        {
            ConfigsCache.Clear();

            var worldItemsNames = EnumHelper.GetWorldItemsNames();
            var worldItemsCount = EnumHelper.GetWorldItemsCount();


            foreach (var worldItem in worldItems)
            {
                if (!worldItemsNames.Contains(worldItem.itemName))
                {
                    Debug.LogWarning(
                        $" {worldItem.itemName} not in world items enum ({worldItem.worldItemType}). You must check name of the object or add it to the enum. ");
                    continue;
                }

                ConfigsCache.Add(worldItem.itemName, worldItem);
            }

            if (ConfigsCache.Count != worldItemsCount)
            {
                foreach (var itemName in worldItemsNames)
                {
                    if (!ConfigsCache.ContainsKey(itemName))
                    {
                        Debug.LogError(
                            $"{itemName} in enums config but not in world items configs list! You must add config for it and add to config list ({name}).");
                    }
                }

                throw new Exception(
                    $"Not all game items in config. In cache {ConfigsCache.Count}, in enums {worldItemsCount}");
            }
            else
            {
                Debug.LogWarning($"<color=green>WorldItemsList has all items in config.</color>");
            }
        }

#if !UNITY_EDITOR
        // Important
        private void Awake() => OnValidate();
#endif
    }
}
