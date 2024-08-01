using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        [SerializeField] private AssetReferenceTexture2D checkIcon;
        [SerializeField] private AssetReferenceTexture2D crossIcon;

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;

        private PopUpFrameUI _popUpFrame;
        private TemplateContainer _buildingPanel;
        private Button _buildButton;
        private BuildingPanelElementsRef _buildingPanelElementsRef;
        private GameDataManager _gameDataManager;

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
        private const string ResIsEnoughIconContainerName = "is-enough-icon-container";

        private const string OtherTypeHeadLabelName = "other-head-label";
        private const string OtherItemLabelName = "other-item-label";
        private const string OtherItemCountLabelName = "other-item-count-label";

        [Inject]
        private void Construct(UIFrameController uiFrameController, BuildingPanelFiller buildingPanelFiller,
            GameDataManager gameDataManager)
        {
            _uiFrameController = uiFrameController;
            _buildingPanelFiller = buildingPanelFiller;
            _gameDataManager = gameDataManager;
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
                ReqResIsEnoughIconContainerName = ResIsEnoughIconContainerName,

                OtherTypeTemplate = buildingOtherTypeItemTemplate,
                OtherItemTemplate = buildingOtherItemTemplate,
                OtherTypeHeadLabelName = OtherTypeHeadLabelName,
                OtherItemLabelName = OtherItemLabelName,
                OtherItemCountLabelName = OtherItemCountLabelName,
                
                CheckIcon = checkIcon,
                CrossIcon = crossIcon
            };

            _buildButton = _buildingPanelElementsRef.BuildButton;
        }

        public void OnBuildZoneEnter(in SWorldItemConfigNew worldItemConfig)
        {
            Debug.LogWarning("OnBuildZoneEnter");
            SubscribeBuildButton(true);

            Dictionary<EReqType, Dictionary<SItemConfig, int>> level = worldItemConfig.GetLevelReq(ELevel.Level_1);

            var isEnough = _gameDataManager.IsEnoughForBuild(level);

            _buildButton.SetEnabled(isEnough);

            _buildingPanelFiller.Fill(level, in _buildingPanelElementsRef, in worldItemConfig);

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
        public string ReqResIsEnoughIconContainerName;
        public AssetReferenceTexture2D CheckIcon;
        public AssetReferenceTexture2D CrossIcon;
    }
}
