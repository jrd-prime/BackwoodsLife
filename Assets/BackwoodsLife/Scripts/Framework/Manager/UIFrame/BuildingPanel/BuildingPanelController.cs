using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame.BuildingPanel
{
    public class BuildingPanelController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset buildingPanelTemplate;
        [SerializeField] private VisualTreeAsset buildingReqResourceItemTemplate;
        [SerializeField] private VisualTreeAsset buildingOtherTypeItemTemplate;
        [SerializeField] private VisualTreeAsset buildingOtherItemTemplate;

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;

        private PopUpFrameUI _popUpFrame;
        private TemplateContainer _buildingPanel;
        private Button _buildButton;
        private BuildingPanelElementsRef _buildingPanelElementsRef;

        private const string IconContainer = "building-icon";
        private const string NameLabel = "building-name-label";
        private const string DescriptionLabel = "building-description-label";
        private const string ButtonName = "build-button";
        private const string ResourceContainerName = "req-res-container";
        private const string OtherContainerName = "req-other-container";

        private const string OtherTypeContainerName = "other-type-container";
        private const string OtherSubContainerName = "other-sub-container";
        private const string OtherItemContainerName = "other-item-container";

        private const string ResItemIconName = "res-item-icon";
        private const string ResItemCountLabelName = "res-item-count-label";

        private const string OtherTypeHeadLabelName = "other-head-label";
        private const string OtherItemLabelName = "other-item-label";
        private const string OtherItemCountLabelName = "other-item-count-label";

        [Inject]
        private void Construct(UIFrameController uiFrameController, BuildingPanelFiller buildingPanelFiller)
        {
            _uiFrameController = uiFrameController;
            _buildingPanelFiller = buildingPanelFiller;
        }

        private void Awake()
        {
            _buildingPanel = buildingPanelTemplate.Instantiate();

            _buildingPanelElementsRef = new BuildingPanelElementsRef
            {
                IconRef = _buildingPanel.Q<VisualElement>(IconContainer),
                NameRef = _buildingPanel.Q<Label>(NameLabel),
                DescriptionRef = _buildingPanel.Q<Label>(DescriptionLabel),
                BuildButton = _buildingPanel.Q<Button>(ButtonName),
                ResourceContainer = _buildingPanel.Q<VisualElement>(ResourceContainerName),
                OtherContainer = _buildingPanel.Q<VisualElement>(OtherContainerName),
                OtherTypeContainer = _buildingPanel.Q<VisualElement>(OtherTypeContainerName),
                OtherSubContainer = OtherSubContainerName,
                OtherItemContainer = _buildingPanel.Q<VisualElement>(OtherItemContainerName),


                ResourceItemTemplate = buildingReqResourceItemTemplate,
                ReqResItemIconName = ResItemIconName,
                ReqResItemCountLabelName = ResItemCountLabelName,
                OtherTypeTemplate = buildingOtherTypeItemTemplate,
                OtherItemTemplate = buildingOtherItemTemplate,
                OtherTypeHeadLabelName = OtherTypeHeadLabelName,
                OtherItemLabelName = OtherItemLabelName,
                OtherItemCountLabelName = OtherItemCountLabelName
            };

            _buildButton = _buildingPanelElementsRef.BuildButton;
        }

        public void OnBuildZoneEnter(in SWorldItemConfigNew worldItemConfig)
        {
            Debug.LogWarning("OnBuildZoneEnter");
            SubscribeBuildButton(true);

            _buildingPanelFiller.Fill(ELevel.Level_1, in _buildingPanelElementsRef, in worldItemConfig);

            _popUpFrame = _uiFrameController.GetPopUpFrame();
            _popUpFrame.ShowIn(EPopUpSubFrame.Left, ref _buildingPanel);
        }

        private void SubscribeBuildButton(bool b)
        {
            if (b) _buildButton.clicked += OnBuildButtonClicked;

            _buildButton.clicked -= OnBuildButtonClicked;
        }

        private void OnBuildButtonClicked()
        {
            throw new NotImplementedException();
        }

        public void OnBuildZoneExit()
        {
            SubscribeBuildButton(false);
            _popUpFrame?.HideIn(EPopUpSubFrame.Left, in _buildingPanel);
        }
    }

    public struct BuildingPanelElementsRef
    {
        public VisualElement IconRef;
        public Label NameRef;
        public Label DescriptionRef;
        public Button BuildButton;
        public VisualElement ResourceContainer;
        public VisualElement OtherContainer;
        public VisualTreeAsset ResourceItemTemplate;
        public VisualTreeAsset OtherItemTemplate;
        public VisualTreeAsset OtherTypeTemplate;
        public string ReqResItemIconName;
        public string ReqResItemCountLabelName;
        public VisualElement OtherTypeContainer;
        public string OtherSubContainer;
        public VisualElement OtherItemContainer;
        public string OtherTypeHeadLabelName;
        public string OtherItemLabelName;
        public string OtherItemCountLabelName;
    }
}
