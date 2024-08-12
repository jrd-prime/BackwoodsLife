using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActionsPanel;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    public class UseActionsState : IInteractZoneState
    {
        private UseActionsPanelUIController _useActionsPanelUIController;
        private readonly List<UseAction> _useActions;
        private readonly Action<IUseAction> _selectedUseActionCallback;
        private Action<IInteractZoneState> _interactZone;
        public string StateDesc => "Use actions state";

        [Inject]
        private void Construct(UseActionsPanelUIController useActionsPanelUIController)
        {
            Debug.LogWarning("Use actions state construct");
            _useActionsPanelUIController = useActionsPanelUIController;
        }

        public UseActionsState(List<UseAction> useActions, Action<IInteractZoneState> interactZoneCallback)
        {
            Assert.IsNotNull(useActions, "useActions is null");

            _useActions = useActions;
            _interactZone = interactZoneCallback;
            _selectedUseActionCallback += OnSelectedUseActionCallback;
        }

        private void OnSelectedUseActionCallback(IUseAction useAction)
        {
            Debug.LogWarning("Action selected: " + useAction.Description);
            useAction.Activate();

            // _interactZone.Invoke(obj);
        }

        public void Enter(InteractZone interactZone)
        {
            _useActionsPanelUIController.ShowUseActionsWindow(_useActions, _selectedUseActionCallback);
        }

        public void Exit()
        {
            _useActionsPanelUIController.Hide();
        }
    }
}
