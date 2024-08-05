using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Structs.Item
{
    [Serializable]
    public struct UpgradeLevel
    {
        public ELevel level;
        public AssetReferenceGameObject levelPrefabReference;

        [FormerlySerializedAs("requirementsForUpgrading")]
        public RequirementForUpgrade reqForUpgrade;
    }
}
