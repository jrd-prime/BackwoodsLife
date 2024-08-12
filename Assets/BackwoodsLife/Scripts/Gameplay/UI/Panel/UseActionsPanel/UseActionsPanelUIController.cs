using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActionsPanel
{
    public class UseActionsPanelUIController : UIPanelController
    {
        [SerializeField] private VisualTreeAsset btnTemplate;

        private FramePopUp _frame;
        private readonly List<(Button button, EventCallback<ClickEvent> callback)> _buttonsCacheTuple = new();

        public void ShowUseActionsWindow(List<UseAction> useActionConfigs,
            Action<IUseAction> selectedUseActionCallback)
        {
            Assert.IsNotNull(useActionConfigs, "useActionConfigs is null");
            Assert.IsNotNull(selectedUseActionCallback, "selectedUseActionCallback is null");

            var panel = PanelTemplate.Instantiate();

            var buttonsContainer = panel.Q<VisualElement>("btns-container");
            buttonsContainer.Clear();

            foreach (var useAction in useActionConfigs)
            {
                IUseAction action = CustomUseAction.CreateUseAction(useAction.useType);

                var buttonTemplate = btnTemplate.Instantiate();
                buttonTemplate.Q<Label>().text = action.Description;
                var button = buttonTemplate.Q<Button>("craft-button");

                buttonsContainer.Add(buttonTemplate);

                SubscribeButton(selectedUseActionCallback, action, button);
            }

            _frame = UIFrameController.GetPopUpFrame();
            _frame.ShowIn(EPopUpSubFrame.Left, panel);
        }


        public void Hide()
        {
            UnSubscribeButtons();
            _frame?.Hide();
        }

        private void SubscribeButton(Action<IUseAction> selectedUseActionCallback, IUseAction action, Button button)
        {
            EventCallback<ClickEvent> callback = _ => { selectedUseActionCallback.Invoke(action); };
            button.RegisterCallback(callback);
            _buttonsCacheTuple.Add((button, callback));
        }

        private void UnSubscribeButtons()
        {
            foreach (var (button, callback) in _buttonsCacheTuple) button.UnregisterCallback(callback);
        }
    }
}
