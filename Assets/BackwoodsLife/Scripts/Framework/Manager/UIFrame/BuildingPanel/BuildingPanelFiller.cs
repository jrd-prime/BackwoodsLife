using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame.BuildingPanel
{
    public class BuildingPanelFiller : IInitializable
    {
        private IAssetProvider _assetProvider;
        private BuildingPanelElementsRef _panelRef;
        private GameDataManager _gameDataManager;
        private WarehouseData _warehouse;
        private BuildingData _building;
        private SkillData _skill;
        private ToolData _tool;

        [Inject]
        private void Construct(IAssetProvider assetProvider, GameDataManager gameDataManager)
        {
            _assetProvider = assetProvider;
            _gameDataManager = gameDataManager;
        }

        public void Initialize()
        {
            _warehouse = _gameDataManager.Warehouse;
            _building = _gameDataManager.Building;
            _skill = _gameDataManager.Skill;
            _tool = _gameDataManager.Tool;
        }

        public void Fill(Dictionary<EItemData, Dictionary<SItemConfig, int>> level,
            in BuildingPanelElementsRef buildingPanelElementsRef,
            in SWorldItemConfigNew itemConfig)
        {
            _panelRef = buildingPanelElementsRef;
            _panelRef.NameRef.text = itemConfig.name;
            _panelRef.DescriptionRef.text = itemConfig.shortDescription;

            LoadAndSetIcon(_panelRef.IconRef, itemConfig.icon);

            _panelRef.ResourceContainer.Clear();
            _panelRef.OtherContainer.Clear();

            foreach (var levelData in level)
            {
                switch (levelData.Key)
                {
                    case EItemData.Resorce:
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
            Debug.LogWarning("Fill resource");

            Dictionary<SItemConfig, int> itemTempDict = new();

            foreach (var pair in levValue)
            {
                var resourceContainer = _panelRef.ResourceItemTemplate.Instantiate();
                var icon = resourceContainer.Q<VisualElement>(_panelRef.ReqResItemIconName);
                var label = resourceContainer.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var isEnough = resourceContainer.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

                itemTempDict.Add(pair.Key, pair.Value);
                SetIsEnoughIcon(isEnough, EItemData.Resorce, ref itemTempDict, true);

                LoadAndSetIcon(icon, pair.Key.iconReference);

                SetText(label, ReqStatText(EItemData.Resorce, pair.Key.itemName, pair.Value));

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
            var itemDataHolder = ChooseDataHolder(itemDataType).GetItem(itemName);
            return itemDataHolder.Count < reqValue ? $"{itemDataHolder.Count}/{reqValue}" : $"{reqValue}/{reqValue}";
        }

        private void FillReqForOther(EItemData itemDataType, Dictionary<SItemConfig, int> levValue)
        {
            Debug.LogWarning("Fill other");

            // Create type template (Building, Skill, Tool)
            var typeTemplate = _panelRef.OtherTypeTemplate.Instantiate();

            // Set type name
            typeTemplate.Q<Label>(_panelRef.OtherTypeHeadLabelName).text = GetReqTypeHead(itemDataType);

            // Find sub container
            var subContainer = typeTemplate.Q<VisualElement>(_panelRef.OtherSubContainer);
            var isEnough = typeTemplate.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

            SetIsEnoughIcon(isEnough, itemDataType, ref levValue);

            // Fill sub container
            foreach (var i in levValue)
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
            ref Dictionary<SItemConfig, int> itemDictionary,
            bool clearDict = false)
        {
            LoadAndSetIcon(iconHolder, ChooseDataHolder(itemDataType).IsEnough(itemDictionary)
                ? _panelRef.CheckIcon
                : _panelRef.CrossIcon);

            if (clearDict) itemDictionary.Clear();
        }

        private ItemDataHolder ChooseDataHolder(EItemData itemDataType)
        {
            return itemDataType switch
            {
                EItemData.Resorce => _warehouse,
                EItemData.Building => _building,
                EItemData.Skill => _skill,
                EItemData.Tool => _tool,
                _ => throw new ArgumentOutOfRangeException(nameof(itemDataType), itemDataType, null)
            };
        }


        private string GetReqTypeHead(EItemData itemDataType)
        {
            return itemDataType switch
            {
                EItemData.Resorce => "Resource",
                EItemData.Building => "Building",
                EItemData.Skill => "Skill",
                EItemData.Tool => "Tool",
                _ => throw new ArgumentOutOfRangeException(nameof(itemDataType), itemDataType, null)
            };
        }
    }
}
