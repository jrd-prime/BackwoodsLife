using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons
{
    public class UIButtonsController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiButtonTemplate;
        [SerializeField] private Sprite questSprite; //TODO remove
        [SerializeField] private Sprite warehouseSprite; //TODO remove

        public Subject<Unit> QuestButtonClicked = new();
        public Subject<Unit> WarehouseButtonClicked = new();

        private UIFrameController _uiFrameController;
        private BuildingPanelFiller _buildingPanelFiller;
        private GameDataManager _gameDataManager;
        private FrameMain _frameMainFrame;


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
            _frameMainFrame = _uiFrameController.GetMainFrame();

            AddButton("Quest", QuestButtonClicked);
            AddButton("Warehouse", WarehouseButtonClicked);
        }

        private void AddButton(string quest, Subject<Unit> onButtonClicked)
        {
            var uiButton = uiButtonTemplate.Instantiate();
            uiButton.Q<Label>().text = quest;

            if (quest == "Quest")
            {
                uiButton.Q<VisualElement>("icon").style.backgroundImage = new StyleBackground(questSprite);
            }
            else if (quest == "Warehouse")
            {
                uiButton.Q<VisualElement>("icon").style.backgroundImage = new StyleBackground(warehouseSprite);
            }


            var btn = uiButton.Q<Button>("ui-button");
            btn.clicked += () => { onButtonClicked.OnNext(Unit.Default); };


            _frameMainFrame.ShowIn(EMainSubFrame.Top, uiButton, false);
        }
    }
}
