﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.InteractableItem;
using BackwoodsLife.Scripts.Framework.InteractableItem.Custom;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.System.Item;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System.WorldItem
{
    /// <summary>
    /// Placed on character prefab
    /// </summary>
    public class Interact : MonoBehaviour, IDisposable
    {
        public bool IsMoving { get; private set; }
        public event Action<List<ItemData>> OnCollectFinished;
        public event Action<List<ItemData>> OnUseFinished;
        public event Action<List<ItemData>> OnUpgradeFinished;
        public event Action<List<ItemData>> OnUseAndUpgradeFinished;

        private IPlayerViewModel _playerViewModel;
        private IItemSystem _usableSystem;
        private IItemSystem _upgradableSystem;
        private IItemSystem _usableAndUpgradableSystem;
        private Spend _spend;
        private CharacterOverUI _characterOverUIHolder;
        private IConfigManager _configManager;
        private Action _triggerCallback;
        private readonly CompositeDisposable _disposable = new();
        private BuildingPanelUIController _buildingPanelUIController;

        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, Spend spend,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager,
            BuildingPanelUIController buildingPanelUIController)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _characterOverUIHolder = characterOverUIHolder;
            _buildingPanelUIController = buildingPanelUIController;
            _spend = spend;
        }

        private void Awake()
        {
            OnCollectFinished += CollectFinished;
            _playerViewModel.MoveDirection
                .Subscribe(x => IsMoving = x.magnitude > 0)
                .AddTo(_disposable);
        }

        public void Interaction(InteractableItemBase interactableItem, Action triggerCallback)
        {
            Debug.LogWarning($"<color=red>Interact system.</color>");

            _triggerCallback = triggerCallback;

            if (interactableItem == null) throw new NullReferenceException("InteractableItem is null");

            switch (interactableItem.interactType)
            {
                case EInteractTypes.Collect:
                    CollectInteraction(interactableItem as Collectable);
                    break;
                case EInteractTypes.Use:
                    break;
                case EInteractTypes.Upgrade:
                    break;
                case EInteractTypes.UseAndUpgrade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void CollectInteraction(Collectable collectable)
        {
            if (collectable == null)
                throw new NullReferenceException("Interactable obj is null. As collectable item");

            var config = _configManager.GetItemConfig<SCollectableItem>(collectable.itemName.ToString());

            await _playerViewModel.SetCollectableActionForAnimationAsync(config.interactAnimation);

            collectable.Process(OnCollectFinished);
        }

        private void CollectFinished(List<ItemData> itemsData)
        {
            _triggerCallback.Invoke();
            _characterOverUIHolder.ShowPopUpFor(itemsData);
        }

        public void OnBuildZoneEnter(in SWorldItemConfig worldItemConfig,
            Action<Dictionary<SItemConfig, int>> buildZoneCallback)
        {
            Debug.LogWarning("Interact system. On build Zone enter");
            _buildingPanelUIController.OnBuildZoneEnter(in worldItemConfig, buildZoneCallback);
        }

        public void OnBuildZoneExit()
        {
            _buildingPanelUIController.OnBuildZoneExit();
        }

        public void Dispose() => _disposable?.Dispose();

        public void SpendResourcesForBuild(Dictionary<SItemConfig, int> levelResources)
        {
            // _spend.Process(levelResources.ToList());
        }
    }
}
