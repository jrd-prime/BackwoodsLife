using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using Cysharp.Threading.Tasks;
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

        public event Action<List<InventoryElement>, EInteractType> OnCollected;


        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder, IConfigManager configManager)
        {
            _configManager = configManager;
            _playerViewModel = playerViewModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
        }

        private void Awake()
        {
            OnCollected += OnCollect;
        }


        public void Interact(ref WorldInteractableItem worldInteractableItem)
        {
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
            _playerViewModel.SetCollectableAction(interactType);

            await UniTask.Delay(5000);
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
    }
}
