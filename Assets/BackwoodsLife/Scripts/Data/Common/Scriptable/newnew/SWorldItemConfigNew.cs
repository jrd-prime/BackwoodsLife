using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
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

        private Dictionary<ELevel, Dictionary<EReqType, Dictionary<SItemConfig, int>>> _reqForUpgradeCache;

        /// <summary>
        /// Dictionary(Level, Dictionary(Req item type, Dictionary(Req type, Req value)))
        /// </summary>
        public Dictionary<ELevel, Dictionary<EReqType, Dictionary<SItemConfig, int>>> UpgradeCache =>
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
                if (upgradeLevel.requirementsForUpgrading.resource.Count > 5)
                    throw new Exception(
                        $"{name} Resource requirements for upgrade level {upgradeLevel.level} is too much. Max 5 resources!");

                if (upgradeLevel.requirementsForUpgrading.building.Count > 3 ||
                    upgradeLevel.requirementsForUpgrading.skill.Count > 3 ||
                    upgradeLevel.requirementsForUpgrading.tool.Count > 3)
                    throw new Exception(
                        $"{name} Building/Tool/Skill requirements for upgrade level {upgradeLevel.level} is too much. Max 3 per type!");
            }

            _reqForUpgradeCache = null;
        }

        private void OnDestroy()
        {
            Debug.LogWarning("Destroy " + name);
            _reqForUpgradeCache.Clear();
        }


        private Dictionary<ELevel, Dictionary<EReqType, Dictionary<SItemConfig, int>>> InitReqForUpgradeCache()
        {
            if (_reqForUpgradeCache is { Count: > 0 }) return _reqForUpgradeCache;

            Debug.LogWarning("Init cache " + name);
            var cache = new Dictionary<ELevel, Dictionary<EReqType, Dictionary<SItemConfig, int>>>();

            foreach (var level in upgradeConfig.upgradeLevels)
            {
                var levelDictionary = new Dictionary<EReqType, Dictionary<SItemConfig, int>>();
                cache.Add(level.level, levelDictionary);

                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.resource, EReqType.Resorce,
                    ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.building, EReqType.Building,
                    ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.tool, EReqType.Tool, ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.skill, EReqType.Skill, ref levelDictionary);
            }


            // foreach (var level in cache)
            // {
            //     Debug.LogWarning($"{level.Key} => {level.Value.Count}");
            //     foreach (var levelReq in level.Value)
            //     {
            //         Debug.LogWarning($"\t{levelReq.Key} => {levelReq.Value.Count}");
            //
            //         foreach (var levelReq2 in levelReq.Value)
            //         {
            //             Debug.LogWarning($"\t\t{levelReq2.Key} => {levelReq2.Value}");
            //         }
            //     }
            // }

            return _reqForUpgradeCache = cache;
        }

        private void AddToDictReqForUpgradeFor<T>(List<CustomRequirement<T>> res, EReqType eReqType,
            ref Dictionary<EReqType, Dictionary<SItemConfig, int>> dict) where T : SItemConfig
        {
            if (res.Count == 0) return;

            dict.Add(eReqType,
                res.ToDictionary<CustomRequirement<T>, SItemConfig, int>(re => re.typeName, re => re.value));
        }

        public Dictionary<EReqType, Dictionary<SItemConfig, int>> GetLevelReq(ELevel level1)
        {
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
