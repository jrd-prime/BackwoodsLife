using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Module.ItemsData;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Building;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Skill;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Tool;
using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel
{
    public class BuildingPanelFiller : IInitializable
    {
        private IAssetProvider _assetProvider;
        private BuildingPanelElementsRef _panelRef;
        private GameDataManager _gameDataManager;
        private WarehouseItemDataModel _warehouseItem;
        private BuildingsDataModel _buildings;
        private SkillsDataModel _skills;
        private ToolsDataModel _tools;

        [Inject]
        private void Construct(IAssetProvider assetProvider, GameDataManager gameDataManager)
        {
            _assetProvider = assetProvider;
            _gameDataManager = gameDataManager;
        }

        public void Initialize()
        {
            _warehouseItem = _gameDataManager.warehouseItem;
            _buildings = _gameDataManager.buildings;
            _skills = _gameDataManager.skills;
            _tools = _gameDataManager.tools;
        }

        public void Fill(Dictionary<EItemData, Dictionary<SItemConfig, int>> level,
            in BuildingPanelElementsRef buildingPanelElementsRef,
            in SWorldItemConfig itemConfig)
        {
            _panelRef = buildingPanelElementsRef;
            _panelRef.NameRef.text = itemConfig.name;
            _panelRef.DescriptionRef.text = itemConfig.shortDescription;

            LoadAndSetIcon(_panelRef.IconRef, itemConfig.iconReference);

            _panelRef.ResourceContainer.Clear();
            _panelRef.OtherContainer.Clear();

            foreach (var levelData in level)
            {
                switch (levelData.Key)
                {
                    case EItemData.Resource:
                        FillReqForResources(levelData.Value);
                        break;
                    case EItemData.Building:
                    case EItemData.Skill:
                    case EItemData.Tool:
                        FillReqForOther(levelData.Key, levelData.Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void FillReqForResources(Dictionary<SItemConfig, int> levValue)
        {
            foreach (var pair in levValue)
            {
                var resourceContainer = _panelRef.ResourceItemTemplate.Instantiate();
                var icon = resourceContainer.Q<VisualElement>(_panelRef.ReqResItemIconName);
                var label = resourceContainer.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var isEnough = resourceContainer.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

                SetIsEnoughIcon(isEnough, EItemData.Resource,
                    new Dictionary<SItemConfig, int> { { pair.Key, pair.Value } });

                LoadAndSetIcon(icon, pair.Key.iconReference);

                SetText(label, ReqStatText(EItemData.Resource, pair.Key.itemName, pair.Value));

                _panelRef.ResourceContainer.Add(resourceContainer);
            }
        }

        private void SetText(Label label, string text) => label.text = text;

        private void LoadAndSetIcon(VisualElement iconHolder, AssetReferenceTexture2D iconReference)
        {
            var icon = _assetProvider.GetIconFromRef(iconReference);
            if (icon == null) throw new ArgumentNullException("Icon not found");
            iconHolder.style.backgroundImage = new StyleBackground(icon);
        }

        private string ReqStatText(EItemData itemDataType, string itemName, int reqValue)
        {
            var itemData = ChooseDataHolder(itemDataType).GetItem(itemName);
            return itemData.Quantity < reqValue
                ? $"{itemData.Quantity}/{reqValue}"
                : $"{reqValue}/{reqValue}";
        }

        private void FillReqForOther(EItemData itemDataType, Dictionary<SItemConfig, int> itemDictionary)
        {
            // Create type template (Building, Skill, Tool)
            var typeTemplate = _panelRef.OtherTypeTemplate.Instantiate();

            // Set type name
            typeTemplate.Q<Label>(_panelRef.OtherTypeHeadLabelName).text = GetReqTypeHead(itemDataType);

            // Find sub container
            var subContainer = typeTemplate.Q<VisualElement>(_panelRef.OtherSubContainer);
            var isEnough = typeTemplate.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

            SetIsEnoughIcon(isEnough, itemDataType, itemDictionary);

            // Fill sub container
            foreach (var i in itemDictionary)
            {
                var itemTemplate = _panelRef.OtherItemTemplate.Instantiate();

                itemTemplate.Q<Label>(_panelRef.OtherItemLabelName).text = i.Key.name;
                itemTemplate.Q<Label>(_panelRef.OtherItemCountLabelName).text =
                    ReqStatText(itemDataType, i.Key.name, i.Value);

                subContainer.Add(itemTemplate);
            }

            _panelRef.OtherContainer.Add(typeTemplate);
        }

        private void SetIsEnoughIcon(VisualElement iconHolder, EItemData itemDataType,
            Dictionary<SItemConfig, int> itemDictionary)
        {
            LoadAndSetIcon(iconHolder, ChooseDataHolder(itemDataType).IsEnough(itemDictionary)
                ? _panelRef.CheckIcon
                : _panelRef.CrossIcon);
        }

        private ItemDataRepository ChooseDataHolder(EItemData itemDataType)
        {
            return itemDataType switch
            {
                EItemData.Resource => _warehouseItem,
                EItemData.Building => _buildings,
                EItemData.Skill => _skills,
                EItemData.Tool => _tools,
                _ => throw new ArgumentOutOfRangeException(nameof(itemDataType), itemDataType, null)
            };
        }


        private string GetReqTypeHead(EItemData itemDataType)
        {
            return itemDataType switch
            {
                EItemData.Resource => "Resource",
                EItemData.Building => "Building",
                EItemData.Skill => "Skill",
                EItemData.Tool => "Tool",
                _ => throw new ArgumentOutOfRangeException(nameof(itemDataType), itemDataType, null)
            };
        }
    }
}
