using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.InteractPanel;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    /// <summary>
    /// Placed on character prefab
    /// </summary>
    public class InteractSystem : MonoBehaviour
    {
        public bool IsMoving { get; private set; }
        public event Action<List<InventoryElement>, EInteractAnimation> OnCollected;

        private CollectSystem _collectSystem;
        private IPlayerViewModel _playerViewModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;
        private CharacterOverUI _characterOverUIHolder;
        private IConfigManager _configManager;
        private Action _triggerCallback;
        private CompositeDisposable _disposable = new();
        private BuildingPanelUIController _buildingPanelUIController;

        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager,
            BuildingPanelUIController buildingPanelUIController)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
            _buildingPanelUIController = buildingPanelUIController;
        }

        private void Awake()
        {
            OnCollected += OnCollect;
            _playerViewModel.MoveDirection
                .Subscribe(x => IsMoving = x.magnitude > 0)
                .AddTo(_disposable);
        }

        public void Interact(ref WorldInteractableItem worldInteractableItem, Action onInteractCompleted)
        {
            Debug.LogWarning($"<color=red>Interact system.</color>");


            _triggerCallback = onInteractCompleted;
            if (worldInteractableItem == null) throw new NullReferenceException("Interactable obj is null");

            switch (worldInteractableItem.interactType)
            {
                case EInteractTypes.Collect:
                    worldInteractableItem.Process(_configManager, _collectSystem, OnCollected);
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

        private async void OnCollect(List<InventoryElement> obj, EInteractAnimation interactAnimation)
        {
            await _playerViewModel.SetCollectableActionForAnimationAsync(interactAnimation);

            _triggerCallback.Invoke();
            _characterOverUIHolder.ShowPopUpFor(obj);
        }

        public void OnBuildZoneEnter(in SWorldItemConfig worldItemConfig, Action onBuildStarted)
        {
            Debug.LogWarning("Interact system. On build Zone enter");
            _buildingPanelUIController.OnBuildZoneEnter(in worldItemConfig, onBuildStarted);
        }

        public void OnBuildZoneExit()
        {
            _buildingPanelUIController.OnBuildZoneExit();
        }
    }
}
