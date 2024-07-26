using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(fileName = "GameItemsList", menuName = SOPathName.ItemsConfigPath + "Game Items list config",
        order = 1)]
    public class SGameItemsList : SItemsConfigList<SGameItemConfig>
    {
        [SerializeField] private List<SGameItemConfig> gameItems;

        private void OnValidate()
        {
            ConfigsCache.Clear();
            foreach (var gameItem in gameItems)
            {
                if (!EnumHelper.GetGameItemsNames().Contains(gameItem.itemName))
                {
                    Debug.LogWarning(
                        $" {gameItem.itemName} not in game items enum ({gameItem.gameItemType}). You must check name of the object or add it to the enum. ");
                    continue;
                }

                ConfigsCache.Add(gameItem.itemName, gameItem);
            }

            if (ConfigsCache.Count != EnumHelper.GetGameItemsCount())
            {
                foreach (var itemName in EnumHelper.GetGameItemsNames())
                {
                    if (!ConfigsCache.ContainsKey(itemName))
                    {
                        Debug.LogError(
                            $"{itemName} in enums config but not in game items configs list! You must add config for it and add to config list ({name}).");
                    }
                }

                throw new Exception(
                    $"Not all game items in config. In cache {ConfigsCache.Count}, in enums {EnumHelper.GetGameItemsCount()}");
            }
            else
            {
                Debug.LogWarning($"<color=green>GameItemsList has all items in config.</color>");
            }
        }

#if !UNITY_EDITOR
        // Important
        private void Awake() => OnValidate();
#endif
    }
}
