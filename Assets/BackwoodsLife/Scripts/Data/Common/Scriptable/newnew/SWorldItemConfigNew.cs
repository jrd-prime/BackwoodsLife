using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.newnew
{
    [CreateAssetMenu(
        fileName = "New world item config",
        menuName = SOPathName.WorldItemPath + "New World Item",
        order = 1)]
    public class SWorldItemConfigNew : ScriptableObject
    {
        /// <summary>
        /// Dictionary(Level, Dictionary(Req item type, Dictionary(Req type, Req value)))
        /// </summary>
        public Dictionary<ELevel, Dictionary<Type, Dictionary<Enum, int>>> reqForUpgradeCache =>
            InitReqForUpgradeCache();

        public EWorldItemNew WorldItemType;
        public EInteractTypes InteractTypes;

        public List<ERequirement> requirementType;

        private Dictionary<ELevel, Dictionary<Type, Dictionary<Enum, int>>> _reqForUpgradeCache;


        private void AddToDictReqForUpgradeFor<T>(List<CustomRequirement<T>> res,
            ref Dictionary<Type, Dictionary<Enum, int>> dict) where T : Enum
        {
            if (res.Count == 0) return;

            dict.Add(typeof(T), res.ToDictionary<CustomRequirement<T>, Enum, int>(re => re.typeName, re => re.value));
        }

        public bool fixedPosition = false;
        [ShowIf("@fixedPosition")] public Vector3 fixedPositionValue;

        [ShowIf("@InteractTypes == EInteractTypes.Collect")]
        public CollectConfig collectConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Use|| InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UseConfig useConfig;

        [ShowIf("@InteractTypes == EInteractTypes.Upgrade || InteractTypes == EInteractTypes.UseAndUpgrade")]
        public UpgradeConfig upgradeConfig;

        private Dictionary<ELevel, Dictionary<Type, Dictionary<Enum, int>>> InitReqForUpgradeCache()
        {
            if (_reqForUpgradeCache is { Count: > 0 }) return _reqForUpgradeCache;

            Debug.LogWarning("Init cache " + name);
            var cache = new Dictionary<ELevel, Dictionary<Type, Dictionary<Enum, int>>>();

            foreach (var level in upgradeConfig.upgradeLevels)
            {
                var levelDictionary = new Dictionary<Type, Dictionary<Enum, int>>();
                cache.Add(level.level, levelDictionary);

                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.resource, ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.building, ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.tool, ref levelDictionary);
                AddToDictReqForUpgradeFor(level.requirementsForUpgrading.skill, ref levelDictionary);
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

        public Dictionary<Type, Dictionary<Enum, int>> GetLevelReq(ELevel level1)
        {
            return reqForUpgradeCache[level1];
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
