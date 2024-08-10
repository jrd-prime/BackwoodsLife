using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Building;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Skill;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Tool;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using VContainer;
using NotImplementedException = System.NotImplementedException;

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
        private void Construct(WarehouseItemDataModel warehouseItemDataModel, BuildingsDataModel buildingsDataModel,
            SkillsDataModel skillsDataModel,
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

        public List<ItemDataWithConfig> CheckRequirementsForCollect(RequirementForCollect requirementsForCollect)
        {
            // TODO refact this sh
            var result = new List<ItemDataWithConfig>();

            foreach (var requirement in requirementsForCollect.building)
            {
                if (!buildings.IsEnough(requirement.typeName.itemName, requirement.value))
                {
                    result.Add(new ItemDataWithConfig { item = requirement.typeName, quantity = requirement.value });
                }
            }

            foreach (var requirement in requirementsForCollect.skill)
            {
                if (!skills.IsEnough(requirement.typeName.itemName, requirement.value))
                {
                    result.Add(new ItemDataWithConfig { item = requirement.typeName, quantity = requirement.value });
                }
            }

            foreach (var requirement in requirementsForCollect.tool)
            {
                if (!tools.IsEnough(requirement.typeName.itemName, requirement.value))
                {
                    result.Add(new ItemDataWithConfig { item = requirement.typeName, quantity = requirement.value });
                }
            }

            return result;
        }
    }
}
