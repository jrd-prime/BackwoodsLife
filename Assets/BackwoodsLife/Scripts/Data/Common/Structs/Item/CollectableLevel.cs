using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct CollectableLevel
    {
        [Range(0, 3)] public int level;
        public AssetReferenceGameObject levelPrefab;
        public List<ItemDataWithConfig> returnedItems;
        public List<ItemDataWithConfig> requiredItems;
    }
}
