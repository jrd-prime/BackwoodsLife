using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame.BuildingPanel
{
    public class BuildingPanelFiller
    {
        private IAssetProvider _assetProvider;
        private BuildingPanelElementsRef _panelRef;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
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
                itemTemplate.Q<Label>(_panelRef.OtherItemCountLabelName).text = i.Value.ToString();
                subContainer.Add(itemTemplate);
            }

            _panelRef.OtherContainer.Add(typeTemplate);
        }


        private void FillReqForResources(Dictionary<SItemConfig, int> levValue)
        {
            Debug.LogWarning("Fill resource");

            foreach (var valuePair in levValue)
            {
                var rr = _panelRef.ResourceItemTemplate.Instantiate();
                var label = rr.Q<Label>(_panelRef.ReqResItemCountLabelName);
                var icon = rr.Q<VisualElement>(_panelRef.ReqResItemIconName);

                label.text = valuePair.Value.ToString();
                icon.style.backgroundImage =
                    new StyleBackground(_assetProvider.GetIconFromRef(valuePair.Key.iconReference));

                _panelRef.ResourceContainer.Add(rr);
            }
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
