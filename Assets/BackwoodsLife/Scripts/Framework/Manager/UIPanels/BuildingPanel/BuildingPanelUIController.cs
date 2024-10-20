﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Const.UI;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanels.BuildingPanel
{
    public class BuildingPanelUIController : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset buildingPanelTemplate;
        [SerializeField] private VisualTreeAsset buildingReqResourceItemTemplate;
        [SerializeField] private VisualTreeAsset buildingOtherTypeItemTemplate;
        [SerializeField] private VisualTreeAsset buildingOtherItemTemplate;
        [SerializeField] private AssetReferenceTexture2D checkIcon;
        [SerializeField] private AssetReferenceTexture2D crossIcon;

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;

        private FramePopUp _framePopUpFrame;
        private TemplateContainer _buildingPanel;
        private Button _buildButton;
        private BuildingPanelElementsRef _buildingPanelElementsRef;
        private GameDataManager _gameDataManager;


        public Action<List<ItemDto>> OnBuildButtonClicked1;
        private Action<Dictionary<ItemSettings, int>> _buildZoneCallback;
        private Dictionary<ItemType, Dictionary<ItemSettings, int>> _currentLevelConfig;

        private readonly List<ItemDto> _tempItemsData = new();

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

        public void ShowBuildingPanelFor(in WorldItemSettings worldItemSettings)
        {
            Debug.LogWarning("OnBuildZoneEnter");


            if (!worldItemSettings.GetLevelReq(LevelType.Level_1, out _currentLevelConfig))
                throw new NullReferenceException(
                    $"Level 1 not found in UpgradeCache. Check config: {worldItemSettings.itemName}");

            _tempItemsData.Clear();

            foreach (var q in _currentLevelConfig[ItemType.Resource])
            {
                _tempItemsData.Add(new ItemDto { Name = q.Key.itemName, Quantity = q.Value });
            }

            _buildButton.clicked += OnBuildButtonClicked;

            var isEnough = _gameDataManager.IsEnoughForBuild(_currentLevelConfig);

            Debug.LogWarning($"IsEnough: {isEnough}");

            _buildButton.SetEnabled(isEnough);

            _buildingPanelFiller.Fill(_currentLevelConfig, in _buildingPanelElementsRef, in worldItemSettings);

            _framePopUpFrame = _uiFrameController.GetPopUpFrame();
            _framePopUpFrame.ShowIn(PopUpSubFrameType.Left, in _buildingPanel);
        }

        public void HideBuildingPanel()
        {
            _buildButton.clicked -= OnBuildButtonClicked;
            _framePopUpFrame?.HideIn(PopUpSubFrameType.Left, in _buildingPanel);
        }

        private void OnBuildButtonClicked() => OnBuildButtonClicked1?.Invoke(_tempItemsData);

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

        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            throw new NotImplementedException();
        }
    }

    public record BuildingPanelElementsRef
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
