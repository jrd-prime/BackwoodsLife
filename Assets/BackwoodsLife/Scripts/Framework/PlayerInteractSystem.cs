using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Framework.Systems;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework
{
    public class PlayerInteractSystem : MonoBehaviour
    {
        private CollectSystem _collectSystem;
        private PlayerModel _playerModel;

        [Inject]
        private void Construct(PlayerModel playerModel, CollectSystem collectSystem)
        {
            _playerModel = playerModel;
            _collectSystem = collectSystem;
        }

        public void Interact(ref InteractableObj interactableObj)
        {
            if (interactableObj == null) throw new NullReferenceException("Interactable obj is null");

            switch (interactableObj.data.interactableType)
            {
                case EInteractableObject.Default:
                    throw new Exception("Interactable type not set. " + interactableObj.name);
                case EInteractableObject.Collectable:

                    var interactableObjData = interactableObj.data as SCollectable;

                    if (interactableObjData == null) throw new NullReferenceException("Interactable obj data is null");

                    if (CheckRequirements(ref interactableObjData.requirementsForCollecting))
                    {
                        _collectSystem.Collect(ref interactableObjData);
                    }

                    break;
                case EInteractableObject.Usable:
                case EInteractableObject.Upgradable:
                case EInteractableObject.UsableAndUpgradable:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CheckRequirements(ref List<Requirement> requirements)
        {
            return false;
        }
    }
}
