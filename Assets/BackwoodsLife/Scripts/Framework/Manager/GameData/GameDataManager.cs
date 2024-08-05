using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
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

        public bool IsEnoughForBuild(Dictionary<EItemData, Dictionary<SItemConfig, int>> level)
        {
            return
                level.TryGetValue(EItemData.Resorce, out var resource)
                    ? Warehouse.IsEnough(resource)
                    : level.TryGetValue(EItemData.Building, out var building)
                        ? Building.IsEnough(building)
                        : level.TryGetValue(EItemData.Skill, out var skill)
                            ? Skill.IsEnough(skill)
                            : !level.TryGetValue(EItemData.Tool, out var tool) || Tool.IsEnough(tool);
        }
    }
}
