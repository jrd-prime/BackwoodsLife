using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse.Resource;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

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
        public List<ERequirement> requirementType;
        public string shortDescription;
        public AssetReferenceTexture2D icon;

        public bool fixedPosition = false;
        [ShowIf("@fixedPosition")] public Vector3 fixedPositionValue;

        [ShowIf("@InteractTypes == EInteractTypes.Collect")]
        public CollectConfig collectConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Use|| InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UseConfig useConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Upgrade || InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UpgradeConfig upgradeConfig;

        private Dictionary<ELevel, Dictionary<EItemData, Dictionary<SItemConfig, int>>> _reqForUpgradeCache;

        /// <summary>
        /// Dictionary(Level, Dictionary(Req item type, Dictionary(Req type, Req value)))
        /// </summary>
        public Dictionary<ELevel, Dictionary<EItemData, Dictionary<SItemConfig, int>>> UpgradeCache =>
            InitReqForUpgradeCache();

        private void OnValidate()
        {
            Assert.IsTrue(icon.RuntimeKeyIsValid(), $"Icon asset ref is not set. World item config: {name}");

            if (InteractTypes == EInteractTypes.Upgrade || InteractTypes == EInteractTypes.UseAndUpgrade)
            {
                if (upgradeConfig.upgradeLevels[0].levelPrefabReference.RuntimeKeyIsValid() == false)
                {
                    throw new Exception($"Upgrade level prefab is not set for level 0. World item config: {name}");
                }
            }


            foreach (var upgradeLevel in upgradeConfig.upgradeLevels)
            {
                if (upgradeLevel.reqForUpgrade.resource.Count > 5)
                    throw new Exception(
                        $"{name} Resource requirements for upgrade level {upgradeLevel.level} is too much. Max 5 resources!");

                if (upgradeLevel.reqForUpgrade.building.Count > 3 ||
                    upgradeLevel.reqForUpgrade.skill.Count > 3 ||
                    upgradeLevel.reqForUpgrade.tool.Count > 3)
                    throw new Exception(
                        $"{name} Building/Tool/Skill requirements for upgrade level {upgradeLevel.level} is too much. Max 3 per type!");

                foreach (var res in upgradeLevel.reqForUpgrade.resource)
                    CheckOnNull(res.typeName, "Resource");
                foreach (var building in upgradeLevel.reqForUpgrade.building)
                    CheckOnNull(building.typeName, "Building");
                foreach (var skill in upgradeLevel.reqForUpgrade.skill)
                    CheckOnNull(skill.typeName, "Skill");
                foreach (var tool in upgradeLevel.reqForUpgrade.tool)
                    CheckOnNull(tool.typeName, "Tool");
            }

            _reqForUpgradeCache = null;
        }

        private void CheckOnNull(SItemConfig resTypeName, string type)
        {
            Assert.IsNotNull(resTypeName, $"{type}: type is null. World item config: {name}");
        }

        private void OnDestroy()
        {
            Debug.LogWarning("Destroy " + name);
            _reqForUpgradeCache.Clear();
        }

        private Dictionary<ELevel, Dictionary<EItemData, Dictionary<SItemConfig, int>>> InitReqForUpgradeCache()
        {
            if (_reqForUpgradeCache is { Count: > 0 }) return _reqForUpgradeCache;

            Debug.LogWarning("Init cache " + name);
            _reqForUpgradeCache = new Dictionary<ELevel, Dictionary<EItemData, Dictionary<SItemConfig, int>>>();

            foreach (var level in upgradeConfig.upgradeLevels)
            {
                var levelDict = new Dictionary<EItemData, Dictionary<SItemConfig, int>>();
                _reqForUpgradeCache.Add(level.level, levelDict);

                AddToDictReqForUpgradeFor(level.reqForUpgrade.resource, EItemData.Resorce, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgrade.building, EItemData.Building, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgrade.tool, EItemData.Tool, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgrade.skill, EItemData.Skill, in levelDict);
            }

            return _reqForUpgradeCache;
        }

        private void AddToDictReqForUpgradeFor<T>(List<CustomRequirement<T>> res, EItemData eItemData,
            in Dictionary<EItemData, Dictionary<SItemConfig, int>> dict) where T : SItemConfig
        {
            if (res.Count == 0) return;

            Debug.LogWarning("e item data = " + eItemData);

            dict.Add(eItemData,
                res.ToDictionary<CustomRequirement<T>, SItemConfig, int>(re => re.typeName, re => re.value));
        }

        public Dictionary<EItemData, Dictionary<SItemConfig, int>> GetLevelReq(ELevel level1)
        {
            Debug.LogWarning("get level req = " + UpgradeCache[level1]);
            return UpgradeCache[level1];
        }
    }

    public enum ERequirement
    {
        Resource = 0,
        Tool = 1,
        Skill = 2,
        Building = 3
    }

    public static class ConfigsExtensions
    {
        public static UpgradeLevel GetLevelRequirements(this SWorldItemConfigNew config, ELevel level)
        {
            Debug.LogWarning($"find level: {level}");

            var levels = config.upgradeConfig.upgradeLevels
                .Where(upgradeLevel => upgradeLevel.level == level)
                .ToArray();

            if (levels.Length == 1) return levels[0];

            throw new NullReferenceException($"{config.name} doesn't contain level {level}");
        }
    }

    [Serializable]
    public struct UpgradeConfig
    {
        public List<UpgradeLevel> upgradeLevels;

        public AssetReferenceGameObject GetLevelPrefabRef(int level)
        {
            return upgradeLevels.Find(x => (int)x.level == level).levelPrefabReference;
        }

        public UpgradeLevel GetLevel(ELevel level)
        {
            return upgradeLevels.Find(x => x.level == level);
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
        PutItems = 1,
        TakeItems = 2,
        Crafting = 3,
        Resting = 4,
        Fishing = 5,
        Drinking = 6,
        Eating = 7
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
