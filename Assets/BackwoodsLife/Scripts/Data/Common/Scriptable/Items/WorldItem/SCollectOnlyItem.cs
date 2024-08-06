using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem
{
    /// <summary>
    /// То что мы можем только собрать. Например, руда
    /// </summary>
    [CreateAssetMenu(
        fileName = "CollectableItem",
        menuName = SOPathName.WorldItemPath + "Collectable Item",
        order = 1)]
    public class SCollectOnlyItem : SWorldItemConfig
    {
        [Title("Collectable Setup")] public List<CollectableLevel> collectableLevels;

        public readonly Dictionary<int, CollectableLevel> CollectableLevelsDict = new();

        [Title("Collect Setup")]public CollectConfig collectConfig;

        protected override void OnValidate()
        {
            base.OnValidate();
            interactTypes = EInteractTypes.Collect;
        }
    }
}
