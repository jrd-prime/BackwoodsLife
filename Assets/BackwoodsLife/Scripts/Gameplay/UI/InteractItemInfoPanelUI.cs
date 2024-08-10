using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI
{
    public class InteractItemInfoPanelUI : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset _itemInfoPanelMainTemplate;
        [SerializeField] private VisualTreeAsset _reqOtherItemTemplate;
        private UIFrameController _uiFrameController;
        private GameDataManager _gameDataManager;
        private TemplateContainer _itemInfoPanel;
        private FramePopUp _framePopUpFrame;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(UIFrameController uiFrameController, GameDataManager gameDataManager,
            IAssetProvider assetProvider)
        {
            _uiFrameController = uiFrameController;
            _gameDataManager = gameDataManager;
            _assetProvider = assetProvider;
        }

        private void Awake()
        {
            _itemInfoPanel = _itemInfoPanelMainTemplate.Instantiate();
            _itemInfoPanel.ToAbsolute();
            // InitializeBuildingPanelElementsReferences();
            // _buildButton = _buildingPanelElementsRef.BuildButton;
        }


        public async void ShowNotEnoughPanelFor(SCollectableItem gg)
        {
            Debug.LogWarning("show panel for not enough requirements");

            //TODO refact this sh*t
            List<ItemDataWithConfigAndActual> notEnoughRequirements =
                _gameDataManager.CheckRequirementsForCollect(gg.collectConfig.requirementForCollect);

            _itemInfoPanel.Q<Label>("building-name-label").text = gg.itemName;

            _itemInfoPanel.Q<VisualElement>("building-icon").style.backgroundImage =
                new StyleBackground(_assetProvider.GetIconFromRef(gg.iconReference));

            var reqOtherContainer = _itemInfoPanel.Q<VisualElement>("req-other-container");

            foreach (var itemData in notEnoughRequirements)
            {
                Debug.LogWarning($"new item {itemData.item}");
                var reqOtherItem = _reqOtherItemTemplate.Instantiate();
                // reqOtherItem.ToAbsolute();

                reqOtherItem.Q<Label>("name").text = itemData.item.itemName;


                var aaa = $"{itemData.actual} / {itemData.required}";
                reqOtherItem.Q<Label>("count").text = aaa;


                var icon = _assetProvider.GetIconFromRef(itemData.item.iconReference);

                reqOtherItem.Q<VisualElement>("icon").style.backgroundImage = new StyleBackground(icon);


                reqOtherContainer.Add(reqOtherItem);
            }

            _framePopUpFrame = _uiFrameController.GetPopUpFrame();
            _framePopUpFrame.ShowIn(EPopUpSubFrame.Left, in _itemInfoPanel);
        }

        public void HidePanel()
        {
            _framePopUpFrame?.HideIn(EPopUpSubFrame.Left, in _itemInfoPanel);
        }


        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            switch (inWindow)
            {
                case "Main": return _itemInfoPanelMainTemplate;
            }

            throw new System.NotImplementedException();
        }
    }
}
