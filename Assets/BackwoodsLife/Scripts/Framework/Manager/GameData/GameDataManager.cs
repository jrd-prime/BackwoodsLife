using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Building;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Skill;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Tool;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.GameData
{
    public class GameDataManager : ILoadingOperation
    {
        public string Description => "Game data manager";

        public WarehouseItemDataModel warehouseItem { get; private set; }
        public BuildingsDataModel buildings { get; private set; }
        public SkillsDataModel skills { get; private set; }
        public ToolsDataModel tools { get; private set; }

        public void ServiceInitialization()
        {
            warehouseItem.Initialize();
            buildings.Initialize();
            skills.Initialize();
            tools.Initialize();
        }

        [Inject]
        private void Construct(WarehouseItemDataModel warehouseItemDataModel, BuildingsDataModel buildingsDataModel, SkillsDataModel skillsDataModel,
            ToolsDataModel toolsDataModel)
        {
            warehouseItem = warehouseItemDataModel;
            buildings = buildingsDataModel;
            skills = skillsDataModel;
            tools = toolsDataModel;
        }

        public bool IsEnoughForBuild(Dictionary<EItemData, Dictionary<SItemConfig, int>> level)
        {
            return
                level.TryGetValue(EItemData.Resource, out var resource)
                    ? warehouseItem.IsEnough(resource)
                    : level.TryGetValue(EItemData.Building, out var building)
                        ? this.buildings.IsEnough(building)
                        : level.TryGetValue(EItemData.Skill, out var skill)
                            ? this.skills.IsEnough(skill)
                            : !level.TryGetValue(EItemData.Tool, out var tool) || this.tools.IsEnough(tool);
        }
    }
}
