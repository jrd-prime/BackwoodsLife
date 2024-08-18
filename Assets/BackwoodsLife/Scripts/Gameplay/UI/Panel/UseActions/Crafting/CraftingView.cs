using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions.Crafting
{
    public class CraftingView : CustomUseActionViewBase<CraftingViewModel>
    {
        [SerializeField] private VisualTreeAsset requiredItemTemplate;

        //TODO add items list template
        private Label _processTitle;
        private VisualElement _infoIcon;
        private Label _infoHead;
        private Label _infoDesc;
        private VisualElement _requiredItems;
        private VisualElement _scrollContainer;

        protected override void InitializeElementsRefs()
        {
            ViewModel.InfoPanelData.Subscribe(SetInfoPanelData).AddTo(Disposables);
            ViewModel.ItemsPanelData.Subscribe(SetItemsPanelData).AddTo(Disposables);
            ViewModel.ProcessPanelData.Subscribe(SetProcessPanelData).AddTo(Disposables);

            Panel = mainTemplate.Instantiate();

            var container = Panel.Q<VisualElement>("crafting-container");

            // Info
            var info = container.Q<VisualElement>("craft-info");
            _infoIcon = info.Q<VisualElement>("icon");
            _infoHead = info.Q<Label>("head");
            _infoDesc = info.Q<Label>("desc");
            _requiredItems = info.Q<VisualElement>("req-list-container");

            // List
            var items = container.Q<VisualElement>("craft-list");
            items.ToAbsolute();
            items.style.backgroundColor = new StyleColor(new Color(0.7f, 0.1f, 0.6f, 1f));

            _scrollContainer = items.Q<ScrollView>("scrl").Q<VisualElement>("unity-content-container");

            _scrollContainer.style.backgroundColor = new StyleColor(new Color(0.5f, 0.5f, 0.5f, 1f));

            // Process
            var process = container.Q<VisualElement>("craft-process");
            _processTitle = process.Q<Label>("title");
        }

        private void SetInfoPanelData(CraftingInfoPanelData craftingInfoPanelData)
        {
            _infoHead.text = craftingInfoPanelData.Title;
            _infoDesc.text = craftingInfoPanelData.Description;
            _infoIcon.style.backgroundImage = new StyleBackground(craftingInfoPanelData.Icon);
        }

        private void SetItemsPanelData(CraftingItemsPanelData craftingItemData)
        {
            foreach (var item in craftingItemData.Items)
            {
                Debug.LogWarning(item);

                var itemElement = requiredItemTemplate.Instantiate();
                itemElement.Q<Label>("head").text = item.Title;
                itemElement.style.backgroundImage = new StyleBackground(item.Icon);
                _scrollContainer.Add(itemElement);
            }
        }


        private void SetProcessPanelData(CraftingProcessPanelData craftingProcessPanelData)
        {
            _processTitle.text = "Crafting process";
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();

            Debug.LogWarning("Close button clicked. View");
        }
    }
}
