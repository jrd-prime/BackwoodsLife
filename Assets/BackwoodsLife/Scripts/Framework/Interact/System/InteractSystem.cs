using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    /// <summary>
    /// Placed on character prefab
    /// </summary>
    public class InteractSystem : MonoBehaviour
    {
        private CollectSystem _collectSystem;
        private PlayerModel _playerModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;
        private CharacterOverUI _characterOverUIHolder;

        public event Action<List<InventoryElement>> OnCollected;


        [Inject]
        private void Construct(PlayerModel playerModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder)
        {
            _playerModel = playerModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
        }

        private void Awake()
        {
            OnCollected += OnCollect;
        }


        private void OnCollect(List<InventoryElement> obj)
        {
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


        public void Interact(ref InteractableObject interactableObject)
        {
            if (interactableObject == null) throw new NullReferenceException("Interactable obj is null");

            switch (interactableObject.data.interactableType)
            {
                case EInteractableObject.Default:
                    throw new Exception("Interactable type not set. " + interactableObject.name);
                case EInteractableObject.Collectable:
                    interactableObject.Process(_collectSystem, OnCollected);
                    break;
                case EInteractableObject.Usable:
                    interactableObject.Process(_usableSystem, OnUse);
                    break;
                case EInteractableObject.Upgradable:
                    interactableObject.Process(_upgradableSystem, OnUpgrade);
                    break;
                case EInteractableObject.UsableAndUpgradable:
                    interactableObject.Process(_usableAndUpgradableSystem, OnUseAndUpgrade);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
