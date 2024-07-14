using System;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Systems
{
    public class PlayerInteractSystem : MonoBehaviour
    {
        private CollectSystem _collectSystem;
        private PlayerModel _playerModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;

        [Inject]
        private void Construct(PlayerModel playerModel, CollectSystem collectSystem)
        {
            _playerModel = playerModel;
            _collectSystem = collectSystem;
        }

        public void Interact(ref InteractableObject interactableObject)
        {
            if (interactableObject == null) throw new NullReferenceException("Interactable obj is null");

            switch (interactableObject.data.interactableType)
            {
                case EInteractableObject.Default:
                    throw new Exception("Interactable type not set. " + interactableObject.name);
                case EInteractableObject.Collectable:
                    interactableObject.Process(_collectSystem);
                    break;
                case EInteractableObject.Usable:
                    interactableObject.Process(_usableSystem);
                    break;
                case EInteractableObject.Upgradable:
                    interactableObject.Process(_upgradableSystem);
                    break;
                case EInteractableObject.UsableAndUpgradable:
                    interactableObject.Process(_usableAndUpgradableSystem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
