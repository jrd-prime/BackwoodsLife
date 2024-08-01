using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
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

        private WarehouseData warehouse;
        private BuildingData building;
        private SkillData skill;
        private ToolData tool;

        [Inject]
        private void Construct(IAssetProvider assetProvider, GameDataManager gameDataManager)
        {
            _assetProvider = assetProvider;
            _gameDataManager = gameDataManager;
        }

        public void Initialize()
        {
            warehouse = _gameDataManager.Warehouse;
            building = _gameDataManager.Building;
            skill = _gameDataManager.Skill;
            tool = _gameDataManager.Tool;
        }

        public void Fill(ELevel levelForFill, in BuildingPanelElementsRef buildingPanelElementsRef,
            in SWorldItemConfigNew worldItemConfig)
        {
            var iconFromRef = _assetProvider.GetIconFromRef(worldItemConfig.icon);

            if (iconFromRef == null) throw new ArgumentNullException("Icon not found for " + worldItemConfig.name);

            _panelRef = buildingPanelElementsRef;
            _panelRef.IconRef.style.backgroundImage = new StyleBackground(iconFromRef);
            _panelRef.NameRef.text = worldItemConfig.name;
            _panelRef.DescriptionRef.text = worldItemConfig.shortDescription;

            Dictionary<EReqType, Dictionary<SItemConfig, int>> level = worldItemConfig.GetLevelReq(levelForFill);

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

            foreach (var valuePair in levValue)
            {
                var rr = _panelRef.ResourceItemTemplate.Instantiate();
                var label = rr.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var icon = rr.Q<VisualElement>(_panelRef.ReqResItemIconName);

                // label.text = valuePair.Value.ToString();

                label.text = ReqStatText(EReqType.Resorce, valuePair.Key.itemName, valuePair.Value);

                icon.style.backgroundImage =
                    new StyleBackground(_assetProvider.GetIconFromRef(valuePair.Key.iconReference));

                _panelRef.ResourceContainer.Add(rr);
            }
        }

        private string ReqStatText(EReqType reqType, string itemName, int reqValue)
        {
            ItemDataHolder data = reqType switch
            {
                EReqType.Resorce => warehouse,
                EReqType.Building => building,
                EReqType.Skill => skill,
                EReqType.Tool => tool,
                _ => throw new ArgumentOutOfRangeException(nameof(reqType), reqType, null)
            };

            Debug.LogWarning($"ask {data.GetType()}");
            var d = data.GetItem(itemName);

            Debug.LogWarning($"{d.Name} {d.Count} / {reqValue}");

            return d.Count < reqValue ? $"{d.Count}/{reqValue}" : $"{reqValue}/{reqValue}";
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
