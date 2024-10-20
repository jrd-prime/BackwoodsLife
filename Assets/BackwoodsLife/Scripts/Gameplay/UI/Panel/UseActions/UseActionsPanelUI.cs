﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions
{
    /// <summary>
    /// Панель выбора метода взаимодействия с <see cref="UsableAndUpgradeable"/> объектом.
    /// После выбора UseAction возвращает созданный экземпляр состояния <see cref="IUseActionViewModel"/> через колбэк в <see cref="UseActionsState"/>
    /// </summary>
    public class UseActionsPanelUI : UIPanel
    {
        [SerializeField] private VisualTreeAsset btnTemplate;

        private FramePopUp _frame;
        private TemplateContainer _cachedPanel;
        private readonly List<(Button button, EventCallback<ClickEvent> callback)> _buttonsCacheTuple = new();

        public void CreateAndShow(ref ItemUseActionsData itemUseActionsData, Action<UseAction> selectedUseAction)
        {
            Assert.IsNotNull(selectedUseAction, "selectedUseActionCallback is null");

            _cachedPanel = CreateAndFillPanel(ref itemUseActionsData, selectedUseAction);
            _frame = UIFrameController.GetPopUpFrame();
            _frame.ShowIn(PopUpSubFrameType.Left, _cachedPanel);
        }

        private TemplateContainer CreateAndFillPanel(ref ItemUseActionsData itemUseActionsData,
            Action<UseAction> selectedUseAction)
        {
            var panel = PanelTemplate.Instantiate();

            var icon = panel.Q<VisualElement>("building-icon");
            icon.style.backgroundImage = new StyleBackground(itemUseActionsData.Icon);

            var title = panel.Q<Label>("building-name-label");
            title.text = itemUseActionsData.Title;

            var desc = panel.Q<Label>("building-description-label");
            desc.text = itemUseActionsData.Description;

            var buttonsContainer = panel.Q<VisualElement>("btns-container");
            buttonsContainer.Clear();

            foreach (var useAction in itemUseActionsData.UseActions)
            {
                var buttonTemplate = btnTemplate.Instantiate();
                buttonTemplate.Q<Label>("head").text = useAction.useType.ToString();
                var button = buttonTemplate.Q<Button>("craft-button");

                buttonsContainer.Add(buttonTemplate);

                SubscribeButton(selectedUseAction, useAction, button);
            }

            return panel;
        }

        public void Show()
        {
            if (_cachedPanel == null) throw new NullReferenceException("Before show panel must be created");
            _frame.ShowIn(PopUpSubFrameType.Left, _cachedPanel);
        }

        public void Hide()
        {
            Debug.LogWarning("Hide use actions panel");
            _frame?.Hide();
        }

        public void HideAndRemove()
        {
            UnSubscribeButtons();
            _frame?.Hide();
            _cachedPanel.Clear();
            _cachedPanel = null;
        }

        //TODO DRY (CraftingView.cs)
        private void SubscribeButton(Action<UseAction> selectedUseActionCallback, UseAction useAction, Button button)
        {
            EventCallback<ClickEvent> callback = _ => { selectedUseActionCallback.Invoke(useAction); };
            button.RegisterCallback(callback);
            _buttonsCacheTuple.Add((button, callback));
        }

        //TODO DRY (CraftingView.cs)
        private void UnSubscribeButtons()
        {
            Debug.LogWarning("Unsubscribe buttons");
            foreach (var (button, callback) in _buttonsCacheTuple) button.UnregisterCallback(callback);
        }
    }
}
