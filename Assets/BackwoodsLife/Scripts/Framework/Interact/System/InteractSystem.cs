using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.InteractPanel;
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

        private const int BuildingStartLevel = 0;
        public event Action<List<InventoryElement>, EInteractType> OnCollected;


        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager, InteractPanelUI interactPanelUI)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
            _interactPanelUI = interactPanelUI;
        }

        private void Awake()
        {
            OnCollected += OnCollect;
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

        private void OnUseAndUpgrade(List<InventoryElement> collectableElements)
        {
            throw new NotImplementedException();
        }

        private void OnUpgrade(List<InventoryElement> collectableElements)
        {
            throw new NotImplementedException();
        }

        private void OnUse(List<InventoryElement> collectableElements)
        {
            throw new NotImplementedException();
        }

        public void Interact(ref SWorldItemConfigNew worldInteractableItem)
        {
            switch (worldInteractableItem.InteractTypes)
            {
                case EInteractTypes.Collect:
                    break;
                case EInteractTypes.Use:
                    break;
                case EInteractTypes.Upgrade:
                    break;
                case EInteractTypes.UseAndUpgrade:
                    _interactPanelUI.Show(worldInteractableItem.InteractTypes);
                    ShowInteractPanel(worldInteractableItem.InteractTypes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            
            //
            // var upgradeConfig = worldItemConfig.upgradeConfig;
            //
            //
            // var prefab = upgradeConfig.GetLevel(BuildingStartLevel);
            //
            // if (prefab == null)
            //     throw new NullReferenceException(
            //         $"{worldItemConfig.name} prefab in upgradeConfig is null for level {BuildingStartLevel + 1}! Check {worldItemConfig.name} config!");
            //
            // Debug.LogWarning(worldItemConfig.fixedPositionValue);
            //
            // var obj = await _assetProvider.InstantiateAsync(prefab);
            //
            // obj.transform.position = worldItemConfig.fixedPositionValue;
            // DestroyImmediate(gameObject);
        }

        private void ShowInteractPanel(EInteractTypes interactTypes)
        {
            throw new NotImplementedException();
        }

        public void Build(ref SWorldItemConfigNew worldItemConfig)
        {
            _interactPanelUI.ShowPanelForBuild(worldItemConfig);
        }
    }
}
