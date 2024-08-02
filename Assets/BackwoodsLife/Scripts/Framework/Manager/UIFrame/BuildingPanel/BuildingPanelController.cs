using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Data.UI;
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
        private Action _buildZoneCallback;

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
            InitializeBuildingPanelElementsReferences();
            _buildButton = _buildingPanelElementsRef.BuildButton;
        }

        public void OnBuildZoneEnter(in SWorldItemConfigNew worldItemConfig, Action onBuildStarted)
        {
            Debug.LogWarning("OnBuildZoneEnter");
            _buildZoneCallback = onBuildStarted;
            _buildButton.clicked += OnBuildButtonClicked;

            Dictionary<EItemData, Dictionary<SItemConfig, int>> level = worldItemConfig.GetLevelReq(ELevel.Level_1);

            var isEnough = _gameDataManager.IsEnoughForBuild(level);

            _buildButton.SetEnabled(isEnough);

            _buildingPanelFiller.Fill(level, in _buildingPanelElementsRef, in worldItemConfig);

            _popUpFrame = _uiFrameController.GetPopUpFrame();
            _popUpFrame.ShowIn(EPopUpSubFrame.Left, in _buildingPanel);
        }

        private void OnBuildButtonClicked()
        {
            Debug.LogWarning("On build button clicked");
            _buildZoneCallback.Invoke();
        }

        public void OnBuildZoneExit()
        {
            _buildButton.clicked -= OnBuildButtonClicked;
            _popUpFrame?.HideIn(EPopUpSubFrame.Left, in _buildingPanel);
        }

        private void InitializeBuildingPanelElementsReferences()
        {
            _buildingPanelElementsRef = new BuildingPanelElementsRef
            {
                IconRef = _buildingPanel.Q<VisualElement>(BuildingPanelConst.IconContainer),
                NameRef = _buildingPanel.Q<Label>(BuildingPanelConst.NameLabel),
                DescriptionRef = _buildingPanel.Q<Label>(BuildingPanelConst.DescriptionLabel),
                BuildButton = _buildingPanel.Q<Button>(BuildingPanelConst.ButtonName),
                ResourceContainer = _buildingPanel.Q<VisualElement>(BuildingPanelConst.ResourceContainerName),
                OtherContainer = _buildingPanel.Q<VisualElement>(BuildingPanelConst.OtherContainerName),
                OtherTypeContainer = _buildingPanel.Q<VisualElement>(BuildingPanelConst.OtherTypeContainerName),
                OtherSubContainer = BuildingPanelConst.OtherSubContainerName,
                OtherItemContainer = _buildingPanel.Q<VisualElement>(BuildingPanelConst.OtherItemContainerName),

                ResourceItemTemplate = buildingReqResourceItemTemplate,
                ReqResItemIconName = BuildingPanelConst.ResItemIconName,
                ReqResItemCountLabelName = BuildingPanelConst.ResItemCountLabelName,
                ReqResIsEnoughIconContainerName = BuildingPanelConst.ResIsEnoughIconContainerName,

                OtherTypeTemplate = buildingOtherTypeItemTemplate,
                OtherItemTemplate = buildingOtherItemTemplate,
                OtherTypeHeadLabelName = BuildingPanelConst.OtherTypeHeadLabelName,
                OtherItemLabelName = BuildingPanelConst.OtherItemLabelName,
                OtherItemCountLabelName = BuildingPanelConst.OtherItemCountLabelName,

                CheckIcon = checkIcon,
                CrossIcon = crossIcon
            };
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
        public VisualTreeAsset OtherTypeTemplate;
        public VisualTreeAsset OtherItemTemplate;
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
