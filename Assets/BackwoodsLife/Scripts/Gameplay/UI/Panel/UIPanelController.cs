﻿using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel
{
    public abstract class UIPanelController : MonoBehaviour, IUIPanelController
    {
        [SerializeField] protected VisualTreeAsset PanelTemplate;
        protected UIFrameController UIFrameController;

        [Inject]
        private void Construct(UIFrameController uiFrameController, BuildingPanelFiller buildingPanelFiller,
            GameDataManager gameDataManager)
        {
            Debug.LogWarning("TEEEESSSTTT TTT T T T T T T ");
            UIFrameController = uiFrameController;
            // _buildingPanelFiller = buildingPanelFiller;
            // _gameDataManager = gameDataManager;
        }

        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            throw new System.NotImplementedException();
        }
    }
}
