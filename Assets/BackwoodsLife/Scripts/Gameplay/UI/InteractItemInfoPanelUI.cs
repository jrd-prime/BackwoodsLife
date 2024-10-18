using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanels;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI
{
    public class InteractItemInfoPanelUI : MonoBehaviour, IUIPanelController
    {
        [SerializeField] private VisualTreeAsset itemInfoPanelMainTemplate;
        [SerializeField] private VisualTreeAsset reqOtherItemTemplate;
        
        private UIFrameController _uiFrameController;
        private GameDataManager _gameDataManager;
        private TemplateContainer _itemInfoPanel;
        private FramePopUp _framePopUpFrame;
        private IAssetProvider _assetProvider;
        private const string ReqOtherContainer = "req-other-container";

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
            _itemInfoPanel = itemInfoPanelMainTemplate.Instantiate();
            _itemInfoPanel.ToAbsolute();
        }

        public void ShowNotEnoughPanelFor(CollectableItem itemConfig)
        {
            var notEnoughRequirements =
                _gameDataManager.CheckRequirementsForCollect(itemConfig.collectConfig
                    .requiredForCollectDto); //TODO refact this sh*t

            _itemInfoPanel.Q<Label>("panel-name-label").text = itemConfig.itemName;

            var panelIcon = _assetProvider.GetIconFromRef(itemConfig.iconReference);
            _itemInfoPanel.Q<VisualElement>("panel-icon").style.backgroundImage = new StyleBackground(panelIcon);

            var reqOtherContainer = _itemInfoPanel.Q<VisualElement>(ReqOtherContainer);

            foreach (var itemData in notEnoughRequirements)
            {
                var reqOtherItem = reqOtherItemTemplate.Instantiate();

                reqOtherItem.Q<Label>("name").text = itemData.item.itemName;
                reqOtherItem.Q<Label>("count").text = $"{itemData.actual} / {itemData.required}";

                var itemIcon = _assetProvider.GetIconFromRef(itemData.item.iconReference);
                reqOtherItem.Q<VisualElement>("icon").style.backgroundImage = new StyleBackground(itemIcon);

                reqOtherContainer.Add(reqOtherItem);
            }

            _framePopUpFrame = _uiFrameController.GetPopUpFrame();
            _framePopUpFrame.ShowIn(PopUpSubFrameType.Left, in _itemInfoPanel);
        }

        public void HidePanel()
        {
            _itemInfoPanel.Q<VisualElement>(ReqOtherContainer).Clear();
            _framePopUpFrame?.HideIn(PopUpSubFrameType.Left, in _itemInfoPanel);
        }


        public VisualTreeAsset GetTemplateFor(string inWindow)
        {
            return inWindow switch
            {
                "Main" => itemInfoPanelMainTemplate,
                _ => throw new NotImplementedException()
            };
        }
    }
}
