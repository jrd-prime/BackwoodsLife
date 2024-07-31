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
        [SerializeField] private VisualTreeAsset buildingReqOtherItemTemplate;

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;

        private PopUpFrameUI _popUpFrame;
        private TemplateContainer _buildingPanel;
        private Button _buildButton;

        [Inject]
        private void Construct(UIFrameController uiFrameController, BuildingPanelFiller buildingPanelFiller)
        {
            _uiFrameController = uiFrameController;
            _buildingPanelFiller = buildingPanelFiller;
        }

        private void Awake()
        {
            _buildingPanel = buildingPanelTemplate.Instantiate();
            _buildButton = _buildingPanel.Q<Button>("build-button");
        }

        public void OnBuildZoneEnter(in SWorldItemConfigNew worldItemConfig)
        {
            Debug.LogWarning("OnBuildZoneEnter");
            SubscribeBuildButton(true);


            _buildingPanelFiller.Fill(ELevel.Level_1, in _buildingPanel, in worldItemConfig);


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
            _popUpFrame.HideIn(EPopUpSubFrame.Left, in _buildingPanel);
        }
    }
}
