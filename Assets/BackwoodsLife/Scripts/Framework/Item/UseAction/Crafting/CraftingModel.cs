using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using R3;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingModel : IUseActionModel<CraftingPanelData>, ICraftingReactive
    {
        public ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; } = new();
        public ReactiveProperty<CraftingInfoPanelData> InfoPanelData { get; } = new();
        public ReactiveProperty<CraftingItemsPanelData> ItemsPanelData { get; } = new();
        public ReactiveProperty<CraftingProcessPanelData> ProcessPanelData { get; } = new();

        public PanelDescriptionData GetDescriptionData(SWorldItemConfig worldItemConfig)
        {
            return new PanelDescriptionData { Title = worldItemConfig.itemName, Description = "crafting description" };
        }

        public CraftingPanelData GetMainPanelData()
        {
            return new CraftingPanelData();
        }

        public void SetDataTo(SWorldItemConfig worldItemConfig)
        {
            DescriptionPanelData.Value = GetDescriptionData(worldItemConfig);
            InfoPanelData.Value = GetInfoPanelData(worldItemConfig);
            ItemsPanelData.Value = GetItemsPanelData(worldItemConfig);
            ProcessPanelData.Value = GetProcessPanelData(worldItemConfig);
        }

        private CraftingProcessPanelData GetProcessPanelData(SWorldItemConfig worldItemConfig)
        {
            return new CraftingProcessPanelData
            {
            };
        }

        private CraftingItemsPanelData GetItemsPanelData(SWorldItemConfig worldItemConfig)
        {
            return new CraftingItemsPanelData
            {
                Items = new List<CraftingItemData>()
            };
        }

        private CraftingInfoPanelData GetInfoPanelData(SWorldItemConfig worldItemConfig)
        {
            return new CraftingInfoPanelData { Title = "crafting title", Description = "crafting description" };
        }
    }
}
