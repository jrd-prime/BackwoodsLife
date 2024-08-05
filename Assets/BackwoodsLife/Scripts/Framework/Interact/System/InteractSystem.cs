using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Inventory;
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
        private CollectSystem _collectSystem;
        private IPlayerViewModel _playerViewModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;
        private CharacterOverUI _characterOverUIHolder;
        private IConfigManager _configManager;
        private Action _triggerCallback;
        private InteractPanelUI _interactPanelUI;

        private CompositeDisposable _disposable = new CompositeDisposable();
        private BuildingPanelUIController _buildingPanelUIController;

        private const int BuildingStartLevel = 0;

        public bool IsMoving { get; private set; }

        public event Action<List<InventoryElement>, EInteractType> OnCollected;


        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager, InteractPanelUI interactPanelUI,
            BuildingPanelUIController buildingPanelUIController)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
            _interactPanelUI = interactPanelUI;
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
            _triggerCallback = onInteractCompleted;
            if (worldInteractableItem == null) throw new NullReferenceException("Interactable obj is null");

            switch (worldInteractableItem.worldItemType)
            {
                case EWorldItem.Collectable:
                    worldInteractableItem.Process(_configManager, _collectSystem, OnCollected);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void OnCollect(List<InventoryElement> obj, EInteractType interactType)
        {
            await _playerViewModel.SetCollectableActionForAnimationAsync(interactType);

            _triggerCallback.Invoke();
            _characterOverUIHolder.ShowPopUpFor(obj);
        }

        private void ShowInteractPanel(EInteractTypes interactTypes)
        {
            throw new NotImplementedException();
        }

        public void OnBuildZoneEnter(in SWorldItemConfigNew worldItemConfig, Action onBuildStarted)
        {
            // Debug.LogWarning("Interact system. On build Zone enter");
            _buildingPanelUIController.OnBuildZoneEnter(in worldItemConfig, onBuildStarted);
        }

        public void OnBuildZoneExit()
        {
            _buildingPanelUIController.OnBuildZoneExit();
        }
    }
}
