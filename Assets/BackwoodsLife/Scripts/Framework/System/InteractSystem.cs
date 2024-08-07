using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Framework.Interact.Unit.Custom;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.System;
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
    public class InteractSystem : MonoBehaviour, IDisposable
    {
        public bool IsMoving { get; private set; }
        public event Action<List<InventoryElement>> OnCollectFinished;
        public event Action<List<InventoryElement>> OnUseFinished;
        public event Action<List<InventoryElement>> OnUpgradeFinished;
        public event Action<List<InventoryElement>> OnUseAndUpgradeFinished;

        private CollectSystem _collectSystem;
        private IPlayerViewModel _playerViewModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;
        private SpendSystem _spendSystem;
        private CharacterOverUI _characterOverUIHolder;
        private IConfigManager _configManager;
        private Action _triggerCallback;
        private CompositeDisposable _disposable = new();
        private BuildingPanelUIController _buildingPanelUIController;

        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, CollectSystem collectSystem, SpendSystem spendSystem,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager,
            BuildingPanelUIController buildingPanelUIController)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
            _buildingPanelUIController = buildingPanelUIController;
            _spendSystem = spendSystem;
        }

        private void Awake()
        {
            OnCollectFinished += CollectFinished;
            _playerViewModel.MoveDirection
                .Subscribe(x => IsMoving = x.magnitude > 0)
                .AddTo(_disposable);
        }

        public void Interact(WorldInteractableItem worldInteractableItem, Action onInteractCompleted)
        {
            Debug.LogWarning($"<color=red>Interact system.</color>");

            _triggerCallback = onInteractCompleted;

            if (worldInteractableItem == null) throw new NullReferenceException("Interactable obj is null");

            switch (worldInteractableItem.interactType)
            {
                case EInteractTypes.Collect:
                    CollectInteraction(worldInteractableItem);
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

        private async void CollectInteraction(WorldInteractableItem interactableItem)
        {
            CollectableItem item = interactableItem as CollectableItem;
            if (item == null)
                throw new NullReferenceException("Interactable obj is null. As collectable item");

            var config = _configManager.GetItemConfig<SCollectOnlyItem>(item.itemName.ToString());

            await _playerViewModel.SetCollectableActionForAnimationAsync(config.interactAnimation);

            interactableItem.Process(_configManager, _collectSystem, OnCollectFinished);
        }

        private void CollectFinished(List<InventoryElement> obj)
        {
            _triggerCallback.Invoke();
            _characterOverUIHolder.ShowPopUpFor(obj);
        }

        public void OnBuildZoneEnter(in SWorldItemConfig worldItemConfig, Action<Dictionary<SItemConfig, int>> buildZoneCallback)
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
            _spendSystem.Spend(levelResources.ToList());
        }
    }
}
