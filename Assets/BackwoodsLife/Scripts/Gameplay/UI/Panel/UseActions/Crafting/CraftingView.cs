using System;
using UnityEngine;
using UnityEngine.UIElements;
using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingView : CustomUseActionViewBase<CraftingViewModel>
    {
        [SerializeField] private VisualTreeAsset requiredItemTemplate;

        protected override void InitializeElementsRefs()
        {
            Panel = mainTemplate.Instantiate();

            var container = Panel.Q<VisualElement>("crafting-container");

            // Info
            var info = container.Q<VisualElement>("craft-info");
            var infoIcon = info.Q<VisualElement>("icon");
            var infoHead = info.Q<Label>("head");
            var infoDesc = info.Q<Label>("desc");
            var requiredItems = info.Q<VisualElement>("req-list-container");

            // List
            var items = container.Q<VisualElement>("craft-list");
            var scrollContainer = items.Q<VisualElement>("unity-content-container");

            // Process
            var process = container.Q<VisualElement>("craft-process");
        }

        protected override void Show(bool s)
        {
            Debug.LogWarning("show = " + s);
            if (!s) return;
            if (Panel == null) throw new NullReferenceException("Panel is null");
            UIFrameController.ShowMainPopUpWindowWithScroll(Panel);
            // UIFrameController.ShowMainPopUpWindow(templateContainer);
            Debug.LogWarning("<color=green>" + ViewModel.Description + "</color>");
        }

        protected override void OnCloseButtonClicked()
        {
            Debug.LogWarning("OnCloseButtonClicked callback in use action view");
            ViewModel.Deactivate();
        }
    }
}
