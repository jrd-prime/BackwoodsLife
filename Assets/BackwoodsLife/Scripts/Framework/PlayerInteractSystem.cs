using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using BackwoodsLife.Scripts.Gameplay.InteractableObjects;
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

        public void Collect(CollectRange interactableCollectRange)
        {
            Debug.LogWarning($"collectSystem:{_collectSystem}");

        }

        public void Interact(ref NewInteractable interactable)
        {
            if (!interactable.HasRequirements)
            {
                if (interactable.Collectable)
                {
                    _collectSystem.Collect(ref interactable);
                }
                
            }
        }
    }
}
