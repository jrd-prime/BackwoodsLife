using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions.Crafting
{
    public sealed class CraftingView : CustomUseActionViewBase<CraftingViewModel>
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
        private readonly List<(Button button, EventCallback<ClickEvent> callback)> _buttonsCacheTuple = new();

        protected override void InitializeElementsRefs()
        {
            Assert.IsNotNull(requiredItemTemplate, "requiredItemTemplate is null");
            Assert.IsNotNull(recipeTemplate, "recipeTemplate is null");

            ViewModel.SelectedRecipePanelData.Subscribe(SetInfoPanelData).AddTo(Disposables);
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
            ClearRecipesContainer();

            // Process
            var process = container.Q<VisualElement>("craft-process");
            _processTitle = process.Q<Label>("title");
        }

        private void ClearRecipesContainer() => _itemsContainer.Clear();

        private void SetInfoPanelData(RecipeInfoData recipeInfoData)
        {
            Debug.LogWarning("SetInfoPanelData");
            _infoHead.text = recipeInfoData.Title;
            _infoDesc.text = recipeInfoData.Description;
            _infoIcon.style.backgroundImage = new StyleBackground(recipeInfoData.Icon);
        }

        private void SetItemsPanelData(CraftingItemsPanelData craftingItemData)
        {
            ClearRecipesContainer();
            Debug.LogWarning("SetItemsPanelData");
            for (int i = 0; i < 4; i++)
            {
                foreach (var item in craftingItemData.Items)
                {
                    // Debug.LogWarning("item = " + item);
                    var itemElement = recipeTemplate.Instantiate();
                    itemElement.Q<Label>("head").text = item.Title;
                    itemElement.Q<VisualElement>("icon").style.backgroundImage = new StyleBackground(item.Icon);
                    _itemsContainer.Add(itemElement);

                    SubscribeButton(item.Title, itemElement.Q<Button>("recipe-btn-container"));
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
            UnSubscribeButtons();
            Debug.LogWarning("Close button clicked. View");
        }

        //TODO DRY (UseActionsPanelUI.cs)
        private void SubscribeButton(string recipeName, Button button)
        {
            Debug.LogWarning($"Subscribe button {recipeName} to {button.name}. VM : {ViewModel.OnRecipeButtonClicked}");
            EventCallback<ClickEvent> callback = _ => { ViewModel.OnRecipeButtonClicked.Invoke(recipeName); };
            button.RegisterCallback(callback);
            _buttonsCacheTuple.Add((button, callback));
        }

        //TODO DRY (UseActionsPanelUI.cs)
        private void UnSubscribeButtons()
        {
            Debug.LogWarning("Unsubscribe buttons");
            foreach (var (button, callback) in _buttonsCacheTuple) button.UnregisterCallback(callback);
        }
    }
}
