using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem
{
    /// <summary>
    /// То что мы можем только улучшать. Например, декоративная постройка
    /// </summary>
    [CreateAssetMenu(
        fileName = "UpgradeOnlyItem",
        menuName = SOPathName.WorldItemPath + "Upgrade Only Item",
        order = 1)]
    public class SUpgradeOnlyItem : SWorldItemConfig
    {
        [Title("Collectable")] [ReadOnly] public int retunedItemsCount;
        [ReadOnly] public int requiredItemsCount;
        [Title("Collectable Setup")] public List<CollectableLevel> collectableLevels;
        public CollectConfig collectConfig;
        public readonly Dictionary<int, CollectableLevel> CollectableLevelsDict = new();
    }
}
