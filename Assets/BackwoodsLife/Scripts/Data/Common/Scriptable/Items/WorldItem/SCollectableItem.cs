using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem
{
    [CreateAssetMenu(
        fileName = "CollectableItem",
        menuName = SOPathName.WorldItemPath + "Collectable Item",
        order = 1)]
    public class SCollectableItem : SWorldItemConfigNew
    {
        [Title("Collectable")] [ReadOnly] public int retunedItemsCount;
        [ReadOnly] public int requiredItemsCount;

        [Title("Collectable Setup")] public List<CollectableLevel> collectableLevels;

        public readonly Dictionary<int, CollectableLevel> CollectableLevelsDict = new();

        protected override void OnValidate()
        {
            base.OnValidate();

            // if (collectableLevels.Count == 0)
            //     throw new Exception("No collectable levels in " + name + ". You need at least one collectable level");

            CollectableLevelsDict.Clear();
            foreach (var level in collectableLevels)
            {
                Assert.IsNotNull(level.levelPrefab, "Level prefab is null." + name);

                if (level.returnedItems.Count > 0)
                {
                    retunedItemsCount = 0;
                    foreach (var returnedItem in level.returnedItems)
                    {
                        Assert.IsNotNull(returnedItem.item, "Returned item config is null." + name);
                        Assert.IsTrue(returnedItem.range.max != 0, "Returned item max range is 0." + name);
                        retunedItemsCount++;
                    }
                }

                if (level.requiredItems.Count > 0)
                {
                    requiredItemsCount = 0;
                    foreach (var requiredItem in level.requiredItems)
                    {
                        Assert.IsNotNull(requiredItem.item, "Required item config is null." + name);
                        Assert.IsTrue(requiredItem.range.max != 0, "Required item value is 0." + name);
                        requiredItemsCount++;
                    }
                }
            }

            foreach (var level in collectableLevels)
            {
                CollectableLevelsDict.Add(level.level, level);
            }
        }
    }
}
