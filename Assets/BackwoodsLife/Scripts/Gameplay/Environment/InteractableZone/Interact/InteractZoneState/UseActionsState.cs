﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    /// <summary>
    /// Состояние <see cref="InteractZone"/>,
    /// в котором октрывается окно (<see cref="UseActionsPanelUI"/>) выбора метода взаимодействия с <see cref="InteractableItem"/>
    /// </summary>
    public sealed class UseActionsState : IInteractZoneState
    {
        public string StateDesc => "Use actions state";

        private Action<IInteractZoneState> _interactZoneStateCallback;
        private Action _onCompleteUseActionCallback;

        private UseActionsPanelUI _useActionsPanelUI;
        private readonly SUseAndUpgradeItem _itemConfig;
        private readonly Action<UseAction> _selectedUseActionCallback;
        private IUseActionViewModel _selectedUseActionViewModel;
        private IObjectResolver _container;

        [Inject]
        private void Construct(IObjectResolver container, UseActionsPanelUI useActionsPanelUI)
        {
            Debug.LogWarning("Use actions state construct");
            _container = container;
            _useActionsPanelUI = useActionsPanelUI;
        }

        public UseActionsState(SUseAndUpgradeItem itemConfig,
            Action<IInteractZoneState> interactZoneStateCallbackCallback)
        {
            Assert.IsNotNull(itemConfig, "itemConfig is null");

            _itemConfig = itemConfig;
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
            _selectedUseActionViewModel.Activate(_itemConfig, _onCompleteUseActionCallback);
        }

        private void OnCompleteUseActionCallback()
        {
            Assert.IsNotNull(_selectedUseActionViewModel, "_selectedUseAction is null");
            _selectedUseActionViewModel.Deactivate();
            _useActionsPanelUI.Show();
        }

        public void Enter(InteractZone interactZone)
        {
            _useActionsPanelUI.CreateAndShow(_itemConfig.useConfig.useActions, _selectedUseActionCallback);
        }

        public void Exit()
        {
            _useActionsPanelUI.HideAndRemove();
        }
    }
}
