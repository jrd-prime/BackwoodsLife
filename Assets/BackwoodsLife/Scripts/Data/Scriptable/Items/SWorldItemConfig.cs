using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Const;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    /// <summary>
    /// Предмет который находится в игровом мире
    /// </summary>
    [CreateAssetMenu(
        fileName = "New world item config",
        menuName = SOPathName.WorldItemPath + "New World Item",
        order = 1)]
    public class WorldItemSettings : ItemSettings
    {
        [Title("World Item Config")] [ReadOnly]
        public EInteractTypes interactTypes;

        [FormerlySerializedAs("interactAnimation")] [Title("Interact Animation Setup")] public InteractAnimationType interactAnimationType;

        [Title("World Position Setup")] public bool fixedPosition;
        [ShowIf("@fixedPosition")] public Vector3 fixedPositionValue;


        [ShowIf("@interactTypes == EInteractTypes.Use || interactTypes == EInteractTypes.UseAndUpgrade")]
        [Title("Use Setup")]
        public UseConfig useConfig;

        [ShowIf("@interactTypes == EInteractTypes.Upgrade || interactTypes == EInteractTypes.UseAndUpgrade")]
        [Title("Upgrade Setup")]
        public UpgradeConfig upgradeConfig;

        private Dictionary<LevelType, Dictionary<ItemType, Dictionary<ItemSettings, int>>> _reqForUpgradeCache;

        /// <summary>
        /// Dictionary(Level, Dictionary(Req item type, Dictionary(Req type, Req value)))
        /// </summary>
        public Dictionary<LevelType, Dictionary<ItemType, Dictionary<ItemSettings, int>>> UpgradeCache =>
            InitReqForUpgradeCache();

        protected override void OnValidate()
        {
            base.OnValidate();
            Assert.IsTrue(interactAnimationType != InteractAnimationType.NotSet,
                $"InteractType is not set. World item config: {name}");
            Assert.IsTrue(iconReference.RuntimeKeyIsValid(), $"Icon asset ref is not set. World item config: {name}");

            if (interactTypes == EInteractTypes.Upgrade || interactTypes == EInteractTypes.UseAndUpgrade)
            {
                if (upgradeConfig.upgradeLevels[0].levelPrefabReference.RuntimeKeyIsValid() == false)
                {
                    throw new Exception($"Upgrade level prefab is not set for level 0. World item config: {name}");
                }
            }


            foreach (var upgradeLevel in upgradeConfig.upgradeLevels)
            {
                if (upgradeLevel.reqForUpgradeDto.resource.Count > 5)
                    throw new Exception(
                        $"{name} Resource requirements for upgrade level {upgradeLevel.levelType} is too much. Max 5 resources!");

                if (upgradeLevel.reqForUpgradeDto.building.Count > 3 ||
                    upgradeLevel.reqForUpgradeDto.skill.Count > 3 ||
                    upgradeLevel.reqForUpgradeDto.tool.Count > 3)
                    throw new Exception(
                        $"{name} Building/Tool/Skill requirements for upgrade level {upgradeLevel.levelType} is too much. Max 3 per type!");
            }

            _reqForUpgradeCache = null;
        }


        private void OnDestroy()
        {
            Debug.LogWarning("Destroy " + name);
            _reqForUpgradeCache.Clear();
        }

        private Dictionary<LevelType, Dictionary<ItemType, Dictionary<ItemSettings, int>>> InitReqForUpgradeCache()
        {
#if UNITY_EDITOR
            _reqForUpgradeCache = null;
#endif
            if (_reqForUpgradeCache is { Count: > 0 }) return _reqForUpgradeCache;

            Debug.LogWarning("Init cache " + name);
            _reqForUpgradeCache = new Dictionary<LevelType, Dictionary<ItemType, Dictionary<ItemSettings, int>>>();

            foreach (var level in upgradeConfig.upgradeLevels)
            {
                var levelDict = new Dictionary<ItemType, Dictionary<ItemSettings, int>>();
                _reqForUpgradeCache.Add(level.levelType, levelDict);

                AddToDictReqForUpgradeFor(level.reqForUpgradeDto.resource, ItemType.Resource, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgradeDto.building, ItemType.Building, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgradeDto.tool, ItemType.Tool, in levelDict);
                AddToDictReqForUpgradeFor(level.reqForUpgradeDto.skill, ItemType.Skill, in levelDict);
            }

            return _reqForUpgradeCache;
        }

        private void AddToDictReqForUpgradeFor<T>(List<RequiredItemSettings<T>> res, ItemType itemType,
            in Dictionary<ItemType, Dictionary<ItemSettings, int>> dict) where T : ItemSettings
        {
            if (res.Count == 0) return;
            dict.Add(itemType,
                res.ToDictionary<RequiredItemSettings<T>, ItemSettings, int>(re => re.itemSettings, re => re.value));
        }

        public bool GetLevelReq(LevelType level1, out Dictionary<ItemType, Dictionary<ItemSettings, int>> level)
        {
            if (UpgradeCache.ContainsKey(level1))
            {
                level = UpgradeCache[level1];
                return true;
            }

            level = null;
            return false;
        }
    }

    public static class ConfigsExtensions
    {
        public static UpgradeLevel GetLevelRequirements(this WorldItemSettings settings, LevelType levelType)
        {
            Debug.LogWarning($"find level: {levelType}");

            var levels = settings.upgradeConfig.upgradeLevels
                .Where(upgradeLevel => upgradeLevel.levelType == levelType)
                .ToArray();

            if (levels.Length == 1) return levels[0];

            throw new NullReferenceException($"{settings.name} doesn't contain level {levelType}");
        }
    }

    [Serializable]
    public struct UpgradeConfig
    {
        public List<UpgradeLevel> upgradeLevels;

        public AssetReferenceGameObject GetLevelPrefabRef(int level)
        {
            return upgradeLevels.Find(x => (int)x.levelType == level).levelPrefabReference;
        }

        public UpgradeLevel GetLevel(LevelType levelType)
        {
            return upgradeLevels.Find(x => x.levelType == levelType);
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
        [FormerlySerializedAs("useActionLevel")] public ProductionLevelType useActionLevelType;
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
        public List<ItemDataWithConfigAndRange> returnedItems;
        [FormerlySerializedAs("requirementForCollectDto")] [FormerlySerializedAs("requirementForCollect")] public RequiredForCollectDto requiredForCollectDto;
    }

    public enum EInteractTypes
    {
        Collect = 0,
        Use = 1,
        Upgrade = 2,
        UseAndUpgrade = 3,
    }
}
