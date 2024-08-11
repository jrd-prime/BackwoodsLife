using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActionsPanel
{
    public class UseActionsPanelUIController : UIPanelController
    {
        [SerializeField] private VisualTreeAsset btnTemplate;
        private readonly List<(Button button, EventCallback<ClickEvent> callback)> _btns = new();

        private FramePopUp _frame;

        public void ShowUseActionsWindow(List<UseAction> useConfigUseActions,
            Action<UseAction> selectedUseActionCallback)
        {
            Assert.IsNotNull(selectedUseActionCallback, "selectedUseActionCallback is null");

            var panel = PanelTemplate.Instantiate();

            var btnContainer = panel.Q<VisualElement>("btns-container");
            btnContainer.Clear();

            foreach (var useAction in useConfigUseActions)
            {
                var btn = btnTemplate.Instantiate();
                btn.Q<Label>().text = useAction.useType.ToString();
                btnContainer.Add(btn);

                var button = btn.Q<Button>("craft-button");

                EventCallback<ClickEvent> callback = _ => { selectedUseActionCallback.Invoke(useAction); };
                button.RegisterCallback(callback);
                _btns.Add((button, callback));
            }

            _frame = UIFrameController.GetPopUpFrame();
            _frame.ShowIn(EPopUpSubFrame.Left, panel);
        }


        public void Hide()
        {
            foreach (var (button, callback) in _btns) button.UnregisterCallback(callback);
            _frame?.Hide();
        }
    }
}
