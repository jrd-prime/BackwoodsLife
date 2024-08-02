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

        public void Fill(Dictionary<EReqType, Dictionary<SItemConfig, int>> level,
            in BuildingPanelElementsRef buildingPanelElementsRef,
            in SWorldItemConfigNew itemConfig)
        {
            var iconFromRef = _assetProvider.GetIconFromRef(itemConfig.icon);

            if (iconFromRef == null) throw new ArgumentNullException("Icon not found for " + itemConfig.name);

            _panelRef = buildingPanelElementsRef;
            _panelRef.IconRef.style.backgroundImage = new StyleBackground(iconFromRef);
            _panelRef.NameRef.text = itemConfig.name;
            _panelRef.DescriptionRef.text = itemConfig.shortDescription;


            _panelRef.ResourceContainer.Clear();
            _panelRef.OtherContainer.Clear();

            foreach (var levelData in level)
            {
                switch (levelData.Key)
                {
                    case EReqType.Resorce:
                        FillReqForResources(levelData.Value);
                        break;
                    case EReqType.Building:
                    case EReqType.Skill:
                    case EReqType.Tool:
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
                var resourceTemplateContainer = _panelRef.ResourceItemTemplate.Instantiate();
                var label = resourceTemplateContainer.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var icon = resourceTemplateContainer.Q<VisualElement>(_panelRef.ReqResItemIconName);
                var isEnough = resourceTemplateContainer.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

                itemTempDict.Add(pair.Key, pair.Value);
                SetIsEnoughIcon(isEnough, EReqType.Resorce, ref itemTempDict, true);

                LoadAndSetIcon(icon, pair.Key.iconReference);

                label.text = ReqStatText(EReqType.Resorce, pair.Key.itemName, pair.Value);


                {
                    ItemDataHolder dataHolder = ChooseDataHolder(EReqType.Resorce);
                    Sprite isEnoughIcon =
                        _assetProvider.GetIconFromRef(dataHolder.IsEnough(pair)
                            ? _panelRef.CheckIcon
                            : _panelRef.CrossIcon);

                    Debug.LogWarning(isEnoughIcon);

                    isEnough.style.backgroundImage = new StyleBackground(isEnoughIcon);
                }

                _panelRef.ResourceContainer.Add(resourceTemplateContainer);
            }
        }

        private void LoadAndSetIcon(VisualElement iconHolder, AssetReferenceTexture2D iconReference)
        {
            var icon = _assetProvider.GetIconFromRef(iconReference);
            iconHolder.style.backgroundImage = new StyleBackground(icon);
        }

        private string ReqStatText(EReqType reqType, string itemName, int reqValue)
        {
            var itemData = ChooseDataHolder(reqType).GetItem(itemName);
            return itemData.Count < reqValue ? $"{itemData.Count}/{reqValue}" : $"{reqValue}/{reqValue}";
        }

        private void FillReqForOther(EReqType levKey, Dictionary<SItemConfig, int> levValue)
        {
            Debug.LogWarning("Fill other");

            // Create type template (Building, Skill, Tool)
            var typeTemplate = _panelRef.OtherTypeTemplate.Instantiate();

            // Set type name
            typeTemplate.Q<Label>(_panelRef.OtherTypeHeadLabelName).text = GetReqTypeHead(levKey);

            // Find sub container
            var subContainer = typeTemplate.Q<VisualElement>(_panelRef.OtherSubContainer);
            var isEnough = typeTemplate.Q<VisualElement>(_panelRef.ReqResIsEnoughIconContainerName);

            SetIsEnoughIcon(isEnough, levKey, ref levValue);

            // Fill sub container
            foreach (var i in levValue)
            {
                var itemTemplate = _panelRef.OtherItemTemplate.Instantiate();

                itemTemplate.Q<Label>(_panelRef.OtherItemLabelName).text = i.Key.name;
                itemTemplate.Q<Label>(_panelRef.OtherItemCountLabelName).text =
                    ReqStatText(levKey, i.Key.name, i.Value);

                subContainer.Add(itemTemplate);
            }

            _panelRef.OtherContainer.Add(typeTemplate);
        }

        private void SetIsEnoughIcon(VisualElement isEnough, EReqType levKey, ref Dictionary<SItemConfig, int> levValue,
            bool clearDict = false)
        {
            ItemDataHolder dataHolder = ChooseDataHolder(levKey);

            LoadAndSetIcon(isEnough, dataHolder.IsEnough(levValue) ? _panelRef.CheckIcon : _panelRef.CrossIcon);

            if (clearDict) levValue.Clear();
        }

        private ItemDataHolder ChooseDataHolder(EReqType levKey)
        {
            return levKey switch
            {
                EReqType.Resorce => _warehouse,
                EReqType.Building => _building,
                EReqType.Skill => _skill,
                EReqType.Tool => _tool,
                _ => throw new ArgumentOutOfRangeException(nameof(levKey), levKey, null)
            };
        }


        private string GetReqTypeHead(EReqType reqType)
        {
            return reqType switch
            {
                EReqType.Resorce => "Resource",
                EReqType.Building => "Building",
                EReqType.Skill => "Skill",
                EReqType.Tool => "Tool",
                _ => throw new ArgumentOutOfRangeException(nameof(reqType), reqType, null)
            };
        }
    }
}
