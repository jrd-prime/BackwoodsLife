using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.GameData
{
    public class GameDataManager : ILoadingOperation
    {
        private WarehouseData _warehouseData;
        private BuildingData _buildingData;
        private SkillData _skillData;
        private ToolData _toolData;
        public string Description => "Game data manager";

        public void ServiceInitialization()
        {
            Debug.LogWarning("Game data manager init");
        }

        [Inject]
        private void Construct(WarehouseData warehouseData, BuildingData buildingData, SkillData skillData,
            ToolData toolData)
        {
            _warehouseData = warehouseData;
            _buildingData = buildingData;
            _skillData = skillData;
            _toolData = toolData;
        }
    }
}
