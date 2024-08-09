using System;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.InteractableItem;
using BackwoodsLife.Scripts.Gameplay.Player;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    /// <summary>
    /// При входе игрока получает с родителя данные объекта и InteractSystem
    /// </summary>
    [RequireComponent(typeof(CapsuleCollider))]
    public class TriggerZone : MonoBehaviour
    {
        private Action _onInteractCompleted;

        private void Awake() => _onInteractCompleted += () => gameObject.transform.parent.gameObject.SetActive(false);

        [Inject]
        private void Construct()
        {
            // TODO inject interact system
            Debug.LogWarning("Trigger zone init");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || other.gameObject.layer != (int)JLayers.Player) return;

            Transform parentTransform = transform.parent;

            if (parentTransform != null)
            {
                var interactable = parentTransform.GetComponent<InteractableItemBase>();
                if (interactable == null)
                    throw new NullReferenceException(
                        $"Interactable is null on {parentTransform.name} prefab. You must set to object Interactable component. ");

                Debug.LogWarning($"<color=red>Trigger zone: </color> {interactable.GetType().Name}");

                var interactSystem = other.GetComponent<PlayerView>().interact;
                if (interactSystem == null)
                    throw new NullReferenceException($"PlayerInteractSystem is null on {other.name}");

                interactSystem.Interaction(interactable, _onInteractCompleted);
            }
            else Debug.LogWarning("Текущий объект не имеет родителя.");
        }
    }
}
