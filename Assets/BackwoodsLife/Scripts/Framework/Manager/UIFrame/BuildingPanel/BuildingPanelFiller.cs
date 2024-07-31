using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
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
        private const string iconContainer = "building-icon";
        private const string nameLabel = "building-name-label";
        private const string descriptionLabel = "building-description-label";

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Fill(ELevel levelForFill, in TemplateContainer buildingPanel,
            in SWorldItemConfigNew worldItemConfig)
        {
            var a = _assetProvider.LoadIconAsync1("Wood");

            var icon = buildingPanel.Q<VisualElement>(iconContainer);
            icon.style.backgroundImage = new StyleBackground(a);
            var name = buildingPanel.Q<Label>(nameLabel);
            name.text = worldItemConfig.name;

            var description = buildingPanel.Q<Label>(descriptionLabel);
            description.text = worldItemConfig.shortDescription;

            Debug.LogWarning("BuildingPanelFiller.Fill");
            Dictionary<Type, Dictionary<Enum, int>> level = worldItemConfig.GetLevelReq(levelForFill);


            foreach (var lev in level)
            {
                Debug.LogWarning(lev);
            }
        }
    }
}
