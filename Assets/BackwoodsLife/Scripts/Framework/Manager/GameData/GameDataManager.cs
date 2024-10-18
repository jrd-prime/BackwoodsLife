using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Building;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Skill;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Tool;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using UnityEngine;
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

        public bool IsEnoughForBuild(Dictionary<ItemType, Dictionary<ItemSettings, int>> level)
        {
            foreach (var lec in level)
            {
                switch (lec.Key)
                {
                    case ItemType.Resource:
                        foreach (var res in level[ItemType.Resource])
                        {
                            if (!warehouseItem.IsEnough(res.Key.itemName, res.Value)) return false;
                        }

                        break;
                    case ItemType.Building:
                        foreach (var res in level[ItemType.Building])
                        {
                            if (!buildings.IsEnough(res.Key.itemName, res.Value)) return false;
                        }

                        break;
                    case ItemType.Skill:
                        foreach (var res in level[ItemType.Skill])
                        {
                            if (!skills.IsEnough(res.Key.itemName, res.Value)) return false;
                        }

                        break;
                    case ItemType.Tool:
                        foreach (var res in level[ItemType.Tool])
                        {
                            if (!tools.IsEnough(res.Key.itemName, res.Value)) return false;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return true;
        }

        public List<ItemDataWithConfigAndActual> CheckRequirementsForCollect(
            RequiredForCollectDto requiredsForCollectDto)
        {
            // TODO refact this sh
            var result = new List<ItemDataWithConfigAndActual>();

            result.AddRange(from requirement in requiredsForCollectDto.building
                where !buildings.IsEnough(requirement.itemSettings.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.itemSettings, required = requirement.value,
                    actual = buildings.GetValue(requirement.itemSettings.itemName)
                });
            result.AddRange(from requirement in requiredsForCollectDto.skill
                where !skills.IsEnough(requirement.itemSettings.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.itemSettings, required = requirement.value,
                    actual = skills.GetValue(requirement.itemSettings.itemName)
                });

            result.AddRange(from requirement in requiredsForCollectDto.tool
                where !tools.IsEnough(requirement.itemSettings.itemName, requirement.value)
                select new ItemDataWithConfigAndActual()
                {
                    item = requirement.itemSettings, required = requirement.value,
                    actual = tools.GetValue(requirement.itemSettings.itemName)
                });

            return result;
        }

        public bool IsEnoughForCollect(RequiredForCollectDto requireds)
        {
            // TODO refact this sh
            if (requireds.building.Any(req =>
                    !buildings.IsEnough(req.itemSettings.itemName, req.value))) return false;

            if (requireds.skill.Any(req =>
                    !skills.IsEnough(req.itemSettings.itemName, req.value))) return false;

            if (requireds.tool.Any(req =>
                    !tools.IsEnough(req.itemSettings.itemName, req.value))) return false;

            Debug.LogWarning("is enough");
            
            return true;
        }
    }
}
