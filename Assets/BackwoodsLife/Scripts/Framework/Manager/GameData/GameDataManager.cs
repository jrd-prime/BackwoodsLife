using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.GameData
{
    public class GameDataManager : ILoadingOperation
    {
        public string Description => "Game data manager";

        public WarehouseData Warehouse { get; private set; }
        public BuildingData Building { get; private set; }
        public SkillData Skill { get; private set; }
        public ToolData Tool { get; private set; }

        public void ServiceInitialization()
        {
            Debug.LogWarning("Game data manager init");
            Warehouse.Initialize();
            Building.Initialize();
            Skill.Initialize();
            Tool.Initialize();
        }

        [Inject]
        private void Construct(WarehouseData warehouseData, BuildingData buildingData, SkillData skillData,
            ToolData toolData)
        {
            Warehouse = warehouseData;
            Building = buildingData;
            Skill = skillData;
            Tool = toolData;
        }

        public bool IsEnoughForBuild(Dictionary<EReqType, Dictionary<SItemConfig, int>> level)
        {
            return
                Warehouse.IsEnough(level[EReqType.Resorce]) &&
                Building.IsEnough(level[EReqType.Building]) &&
                Skill.IsEnough(level[EReqType.Skill]) &&
                Tool.IsEnough(level[EReqType.Tool]);
        }
    }
}
