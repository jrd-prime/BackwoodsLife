using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.UIButtons
{
    public class UIButtonsController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiButtonTemplate;
        [SerializeField] private Sprite questSprite; //TODO remove
        [SerializeField] private Sprite warehouseSprite; //TODO remove

        public Subject<Unit> QuestButtonClicked = new();
        public Subject<Unit> WarehouseButtonClicked = new();
        public Subject<Unit> ShopButtonClicked = new();
        public Subject<Unit> InventoryButtonClicked = new();
        public Subject<Unit> SkillsButtonClicked = new();
        public Subject<Unit> BuildingsButtonClicked = new();

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

            AddButton(EUIButton.Shop, ShopButtonClicked);
            AddButton(EUIButton.Quest, QuestButtonClicked);
            AddButton(EUIButton.Warehouse, WarehouseButtonClicked);
            AddButton(EUIButton.Inventory, InventoryButtonClicked);
            AddButton(EUIButton.Skills, SkillsButtonClicked);
            AddButton(EUIButton.Buildings, BuildingsButtonClicked);
        }

        private void AddButton(EUIButton quest, Subject<Unit> onButtonClicked)
        {
            var uiButton = uiButtonTemplate.Instantiate();
            uiButton.Q<Label>().text = quest.ToString();

            //TODO polymorphism
            uiButton.Q<VisualElement>("icon").style.backgroundImage = quest switch
            {
                EUIButton.Quest => new StyleBackground(questSprite),
                EUIButton.Warehouse => new StyleBackground(warehouseSprite),
                EUIButton.Shop => new StyleBackground(warehouseSprite),
                EUIButton.Inventory => new StyleBackground(warehouseSprite),
                EUIButton.Skills => new StyleBackground(warehouseSprite),
                EUIButton.Buildings => new StyleBackground(warehouseSprite),
                _ => throw new ArgumentOutOfRangeException(nameof(quest), quest, null)
            };

            var btn = uiButton.Q<Button>("ui-button");
            btn.clicked += () => { onButtonClicked.OnNext(Unit.Default); };


            _frameMainFrame.ShowIn(EMainSubFrame.Top, uiButton, false);
        }
    }

    public enum EUIButton
    {
        Shop = 0,
        Quest = 1,
        Warehouse = 2,
        Inventory = 3,
        Skills = 4,
        Buildings = 5
    }
}
