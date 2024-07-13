using System;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework
{
    public class PlayerInteractSystem : MonoBehaviour
    {
        private CollectSystem _collectSystem;

        [Inject]
        private void Construct(CollectSystem collectSystem)
        {
            _collectSystem = collectSystem;
            Debug.LogWarning($"collectSystem:{_collectSystem}");
        }

        public void Interact(ref InteractableObj interactableObj)
        {
            switch (interactableObj.data.interactableType)
            {
                case EInteractableObjectType.Collectable:
                    // var sInteractableCollectableData = interactable.data as SCollectableData;
                    // _collectSystem.Collect(sInteractableCollectableData);
                    break;
                case EInteractableObjectType.Usable:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
