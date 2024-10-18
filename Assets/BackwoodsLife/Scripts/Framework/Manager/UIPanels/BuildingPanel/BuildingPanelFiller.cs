using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.DataModel;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Building;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Skill;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Tool;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanels.BuildingPanel
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

        public void Fill(Dictionary<ItemType, Dictionary<ItemSettings, int>> level,
            in BuildingPanelElementsRef buildingPanelElementsRef,
            in WorldItemSettings itemSettings)
        {
            _panelRef = buildingPanelElementsRef;
            _panelRef.NameRef.text = itemSettings.name;
            _panelRef.DescriptionRef.text = itemSettings.shortDescription;

            LoadAndSetIcon(_panelRef.IconRef, itemSettings.iconReference);

            _panelRef.ResourceContainer.Clear();
            _panelRef.OtherContainer.Clear();

            foreach (var levelData in level)
            {
                switch (levelData.Key)
                {
                    case ItemType.Resource:
                        FillReqForResources(levelData.Value);
                        break;
                    case ItemType.Building:
                    case ItemType.Skill:
                    case ItemType.Tool:
                        FillReqForOther(levelData.Key, levelData.Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void FillReqForResources(Dictionary<ItemSettings, int> levValue)
        {
            foreach (var pair in levValue)
            {
                var resourceContainer = _panelRef.ResourceItemTemplate.Instantiate();
                var icon = resourceContainer.Q<VisualElement>(_panelRef.ReqResItemIconName);
                var label = resourceContainer.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var isEnough = resourceContainer.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

                SetIsEnoughIcon(isEnough, ItemType.Resource,
                    new Dictionary<ItemSettings, int> { { pair.Key, pair.Value } });

                LoadAndSetIcon(icon, pair.Key.iconReference);

                SetText(label, ReqStatText(ItemType.Resource, pair.Key.itemName, pair.Value));

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

        private string ReqStatText(ItemType itemTypeType, string itemName, int reqValue)
        {
            var itemData = ChooseDataHolder(itemTypeType).GetItem(itemName);
            return itemData.Quantity < reqValue
                ? $"{itemData.Quantity}/{reqValue}"
                : $"{reqValue}/{reqValue}";
        }

        private void FillReqForOther(ItemType itemTypeType, Dictionary<ItemSettings, int> itemDictionary)
        {
            // Create type template (Building, Skill, Tool)
            var typeTemplate = _panelRef.OtherTypeTemplate.Instantiate();

            // Set type name
            typeTemplate.Q<Label>(_panelRef.OtherTypeHeadLabelName).text = GetReqTypeHead(itemTypeType);

            // Find sub container
            var subContainer = typeTemplate.Q<VisualElement>(_panelRef.OtherSubContainer);
            var isEnough = typeTemplate.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

            SetIsEnoughIcon(isEnough, itemTypeType, itemDictionary);

            // Fill sub container
            foreach (var i in itemDictionary)
            {
                var itemTemplate = _panelRef.OtherItemTemplate.Instantiate();

                itemTemplate.Q<Label>(_panelRef.OtherItemLabelName).text = i.Key.name;
                itemTemplate.Q<Label>(_panelRef.OtherItemCountLabelName).text =
                    ReqStatText(itemTypeType, i.Key.name, i.Value);

                subContainer.Add(itemTemplate);
            }

            _panelRef.OtherContainer.Add(typeTemplate);
        }

        private void SetIsEnoughIcon(VisualElement iconHolder, ItemType itemTypeType,
            Dictionary<ItemSettings, int> itemDictionary)
        {
            foreach (var i in itemDictionary)
            {
                // Debug.LogWarning("req = " + i.Key.itemName + " " + i.Value);
            }
            
            // Debug.LogWarning(_gameDataManager.buildings.GetValue("Bonfire") + " v");

            // Debug.LogWarning($"data holder = {ChooseDataHolder(itemDataType)}");

            // Debug.LogWarning($"is enough = {ChooseDataHolder(itemDataType).IsEnough(itemDictionary)}");

            LoadAndSetIcon(iconHolder, ChooseDataHolder(itemTypeType).IsEnough(itemDictionary)
                ? _panelRef.CheckIcon
                : _panelRef.CrossIcon);
        }

        private IDataRepository ChooseDataHolder(ItemType itemTypeType)
        {
            return itemTypeType switch
            {
                ItemType.Resource => _warehouseItem,
                ItemType.Building => _buildings,
                ItemType.Skill => _skills,
                ItemType.Tool => _tools,
                _ => throw new ArgumentOutOfRangeException(nameof(itemTypeType), itemTypeType, null)
            };
        }


        private string GetReqTypeHead(ItemType itemTypeType)
        {
            return itemTypeType switch
            {
                ItemType.Resource => "Resource",
                ItemType.Building => "Building",
                ItemType.Skill => "Skill",
                ItemType.Tool => "Tool",
                _ => throw new ArgumentOutOfRangeException(nameof(itemTypeType), itemTypeType, null)
            };
        }
    }
}
