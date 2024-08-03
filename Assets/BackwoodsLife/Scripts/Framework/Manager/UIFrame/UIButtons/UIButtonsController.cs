using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.BuildingPanel;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons
{
    public class UIButtonsController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiButtonTemplate;

        public Subject<Unit> QuestButtonClicked = new();
        public Subject<Unit> WarehouseButtonClicked = new();

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;
        private GameDataManager _gameDataManager;
        private MainFrameUI _mainFrame;


        [Inject]
        private void Construct(UIFrameController uiFrameController, BuildingPanelFiller buildingPanelFiller,
            GameDataManager gameDataManager)
        {
            _uiFrameController = uiFrameController;
            _buildingPanelFiller = buildingPanelFiller;
            _gameDataManager = gameDataManager;
        }

        private void Start()
        {
            _mainFrame = _uiFrameController.GetMainFrame();

            AddButton("Quest", QuestButtonClicked);
            AddButton("Warehouse", WarehouseButtonClicked);
        }

        private void AddButton(string quest, Subject<Unit> onButtonClicked)
        {
            var uiButton = uiButtonTemplate.Instantiate();
            uiButton.Q<Label>().text = quest;

            var btn = uiButton.Q<Button>("ui-button");
            btn.clicked += () =>
            {
                onButtonClicked.OnNext(Unit.Default);
            };


            _mainFrame.ShowIn(EMainSubFrame.Top, uiButton, false);
        }
    }
}
