using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Structs
{
    [Serializable]
    public struct UpgradeLevel
    {
        [FormerlySerializedAs("level")] public LevelType levelType;
        public AssetReferenceGameObject levelPrefabReference;

        [FormerlySerializedAs("reqForUpgrade")] [FormerlySerializedAs("requirementsForUpgrading")]
        public RequiredForUpgradeDto reqForUpgradeDto;
    }
}
