using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.newnew
{
    [CreateAssetMenu(
        fileName = "New world item config",
        menuName = SOPathName.WorldItemPath + "New World Item",
        order = 1)]
    public class SWorldItemConfigNew : ScriptableObject
    {
        public EWorldItemNew WorldItemType;
        public EInteractTypes InteractTypes;

        public bool fixedPosition = false;
        [ShowIf("@fixedPosition")] public Vector3 fixedPositionValue;

        [ShowIf("@InteractTypes == EInteractTypes.Collect")]
        public CollectConfig collectConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Use|| InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UseConfig useConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Upgrade || InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UpgradeConfig upgradeConfig;
    }

    [Serializable]
    public struct UpgradeConfig
    {
        public List<UpgradeLevel> upgradeLevels;

        public AssetReferenceGameObject GetLevel(int level)
        {
            return upgradeLevels.Find(x => (int)x.level == level).levelPrefabReference;
        }
    }

    /// <summary>
    /// Список действий для использования. <see cref="UseAction"/>
    /// </summary>
    [Serializable]
    public struct UseConfig
    {
        public List<UseAction> useActions;
    }

    [Serializable]
    public struct UseAction
    {
        public EUseType useType;
    }

    public enum EUseType
    {
        Cooking = 0,
        AddItems = 1,
    }

    [Serializable]
    public struct CollectConfig
    {
    }

    public enum EInteractTypes
    {
        Collect = 0,
        Use = 1,
        Upgrade = 2,
        UseAndUpgrade = 3,
    }
}
