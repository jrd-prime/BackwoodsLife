﻿using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Building;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Skill;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Tool;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
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
                        ? buildings.IsEnough(building)
                        : level.TryGetValue(EItemData.Skill, out var skill)
                            ? skills.IsEnough(skill)
                            : !level.TryGetValue(EItemData.Tool, out var tool) || tools.IsEnough(tool);
        }

        public List<ItemDataWithConfigAndActual> CheckRequirementsForCollect(
            RequirementForCollect requirementsForCollect)
        {
            // TODO refact this sh
            var result = new List<ItemDataWithConfigAndActual>();

            result.AddRange(from requirement in requirementsForCollect.building
                where !buildings.IsEnough(requirement.typeName.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.typeName, required = requirement.value,
                    actual = buildings.GetValue(requirement.typeName.itemName)
                });
            result.AddRange(from requirement in requirementsForCollect.skill
                where !skills.IsEnough(requirement.typeName.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.typeName, required = requirement.value,
                    actual = skills.GetValue(requirement.typeName.itemName)
                });

            result.AddRange(from requirement in requirementsForCollect.tool
                where !tools.IsEnough(requirement.typeName.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.typeName, required = requirement.value,
                    actual = tools.GetValue(requirement.typeName.itemName)
                });

            return result;
        }

        public bool IsEnoughForCollect(RequirementForCollect requirements)
        {
            // TODO refact this sh
            if (requirements.building.Any(req =>
                    !buildings.IsEnough(req.typeName.itemName, req.value))) return false;

            if (requirements.skill.Any(req =>
                    !skills.IsEnough(req.typeName.itemName, req.value))) return false;

            if (requirements.tool.Any(req =>
                    !tools.IsEnough(req.typeName.itemName, req.value))) return false;

            return true;
        }
    }
}
