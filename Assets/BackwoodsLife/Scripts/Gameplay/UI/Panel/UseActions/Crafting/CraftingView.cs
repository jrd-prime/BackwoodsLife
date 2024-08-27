using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions.Crafting
{
    public class CraftingView : CustomUseActionViewBase<CraftingViewModel>
    {
        [SerializeField] private VisualTreeAsset requiredItemTemplate;
        [SerializeField] private VisualTreeAsset recipeTemplate;

        //TODO add items list template
        private Label _processTitle;
        private VisualElement _infoIcon;
        private Label _infoHead;
        private Label _infoDesc;
        private VisualElement _requiredItems;
        private VisualElement _itemsContainer;

        protected override void InitializeElementsRefs()
        {
            Assert.IsNotNull(requiredItemTemplate, "requiredItemTemplate is null");
            Assert.IsNotNull(recipeTemplate, "recipeTemplate is null");

            ViewModel.InfoPanelData.Subscribe(SetInfoPanelData).AddTo(Disposables);
            ViewModel.ItemsPanelData.Subscribe(SetItemsPanelData).AddTo(Disposables);
            ViewModel.ProcessPanelData.Subscribe(SetProcessPanelData).AddTo(Disposables);

            Panel = mainTemplate.Instantiate();
            Panel.ToAbsolute();


            var container = Panel.Q<VisualElement>("crafting-container");

            // Info
            var info = container.Q<VisualElement>("craft-info");
            _infoIcon = info.Q<VisualElement>("icon");
            _infoHead = info.Q<Label>("head");
            _infoDesc = info.Q<Label>("desc");
            _requiredItems = info.Q<VisualElement>("req-list-container");

            // List
            var items = container.Q<VisualElement>("craft-list");

            _itemsContainer = items.Q<VisualElement>("items-container");
            _itemsContainer.Clear();
            // _itemsContainer.style.backgroundColor = new StyleColor(new Color(1, 1, 1, 1));


            // Process
            var process = container.Q<VisualElement>("craft-process");
            _processTitle = process.Q<Label>("title");
        }

        private void SetInfoPanelData(CraftingInfoPanelData craftingInfoPanelData)
        {
            Debug.LogWarning("SetInfoPanelData");
            _infoHead.text = craftingInfoPanelData.Title;
            _infoDesc.text = craftingInfoPanelData.Description;
            _infoIcon.style.backgroundImage = new StyleBackground(craftingInfoPanelData.Icon);
        }

        private void SetItemsPanelData(CraftingItemsPanelData craftingItemData)
        {
            Debug.LogWarning("SetItemsPanelData");

            for (int i = 0; i < 4; i++)
            {
                foreach (var item in craftingItemData.Items)
                {
                    Debug.LogWarning(item);

                    var itemElement = recipeTemplate.Instantiate();
                    // itemElement.style.marginBottom = new StyleLength(20f);
                    // itemElement.style.marginLeft = new StyleLength(20f);
                    // itemElement.style.marginRight = new StyleLength(20f);
                    // itemElement.style.marginTop = new StyleLength(20f);
                    itemElement.Q<Label>("head").text = item.Title;
                    itemElement.style.backgroundImage = new StyleBackground(item.Icon);
                    _itemsContainer.Add(itemElement);
                }
            }
        }


        private void SetProcessPanelData(CraftingProcessPanelData craftingProcessPanelData)
        {
            Debug.LogWarning("SetProcessPanelData");
            _processTitle.text = "Crafting process";
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();

            Debug.LogWarning("Close button clicked. View");
        }
    }
}
