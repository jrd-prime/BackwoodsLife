using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    /// <summary>
    /// Состояние <see cref="InteractZone"/>, в котором октрывается окно (<see cref="UseActionsPanelUI"/>) выбора метода взаимодействия с <see cref="InteractableItem"/>
    /// </summary>
    public sealed class UseActionsState : IInteractZoneState
    {
        private UseActionsPanelUI _useActionsPanelUI;
        private readonly List<UseAction> _useActions;
        private readonly Action<UseAction> _selectedUseActionCallback;
        private Action<IInteractZoneState> _interactZoneStateCallback;
        private event Action _onCompleteUseActionCallback;

        private IUseActionViewModel _selectedUseActionViewModel;
        public string StateDesc => "Use actions state";

        private IObjectResolver _container;

        [Inject]
        private void Construct(IObjectResolver container, UseActionsPanelUI useActionsPanelUI)
        {
            Debug.LogWarning("Use actions state construct");
            _container = container;
            _useActionsPanelUI = useActionsPanelUI;
        }

        public UseActionsState(List<UseAction> useActions, Action<IInteractZoneState> interactZoneStateCallbackCallback)
        {
            Assert.IsNotNull(useActions, "useActions is null");

            _useActions = useActions;
            _interactZoneStateCallback = interactZoneStateCallbackCallback;
            _selectedUseActionCallback += OnSelectedUseActionCallback;
            _onCompleteUseActionCallback += OnCompleteUseActionCallback;
        }

        /// <summary>
        /// Активирует состояние выбранного <see cref="IUseActionViewModel"/>
        /// </summary>
        /// <param name="useAction"></param>
        private void OnSelectedUseActionCallback(UseAction useAction)
        {
            Type t = useAction.useType switch
            {
                // EUseType.Cooking => typeof(CookingActionViewModel),
                // EUseType.PutItems => typeof(PutActionViewModel),
                // EUseType.TakeItems => typeof(TakeActionViewModel),
                EUseType.Crafting => typeof(CraftingViewModel),
                // EUseType.Resting => typeof(RestingActionViewModel),
                // EUseType.Fishing => typeof(FishingActionViewModel),
                // EUseType.Drinking => typeof(DrinkingActionViewModel),
                // EUseType.Eating => typeof(BeatingActionViewModel),
                _ => throw new ArgumentOutOfRangeException()
            };


            _selectedUseActionViewModel = _container.Resolve(t) as IUseActionViewModel;
            if (_selectedUseActionViewModel == null)
                throw new NullReferenceException("Selected use action view model is null");

            Debug.LogWarning("Action selected: " + _selectedUseActionViewModel.Description);
            _useActionsPanelUI.Hide();
            _selectedUseActionViewModel.Activate(_onCompleteUseActionCallback);
        }

        private void OnCompleteUseActionCallback()
        {
            Assert.IsNotNull(_selectedUseActionViewModel, "_selectedUseAction is null");
            _selectedUseActionViewModel.Deactivate();
            _useActionsPanelUI.Show();
        }

        public void Enter(InteractZone interactZone)
        {
            _useActionsPanelUI.CreateAndShow(_useActions, _selectedUseActionCallback);
        }

        public void Exit()
        {
            _useActionsPanelUI.HideAndRemove();
        }
    }
}
