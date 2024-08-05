using System;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Gameplay.Player;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class TriggerZone : MonoBehaviour
    {
        private Action _onInteractCompleted;

        private void Awake()
        {
            _onInteractCompleted += () => { gameObject.transform.parent.gameObject.SetActive(false); };
        }

        [Inject]
        private void Construct()
        {
            Debug.LogWarning("Trigger zone init");
        }

        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что объект, вошедший в зону триггера, находится на слое Player
            if (other.gameObject.layer != (int)JLayers.Player) return;

            Debug.Log($"Char in trigger zone! {name}");

            if (other != null)
            {
                Transform parentTransform = transform.parent;

                if (parentTransform != null)
                {
                    var interactable = transform.parent.GetComponent<WorldInteractableItem>();

                    if (interactable == null)
                        throw new NullReferenceException(
                            $"Interactable is null on {parentTransform.name} prefab. You must set to object Interactable component. ");

                    // var playerInteractSystem = other.GetComponent<InteractSystem>();
                    // if (playerInteractSystem == null)
                    //     throw new NullReferenceException($"PlayerInteractSystem is null on {other.name}");


                    var playerInteractSystem = other.GetComponent<PlayerView>().InteractSystem;
                    if (playerInteractSystem == null)
                        throw new NullReferenceException($"PlayerInteractSystem is null on {other.name}");

                    playerInteractSystem.Interact(ref interactable, _onInteractCompleted);
                }
                else
                {
                    Debug.LogWarning("Текущий объект не имеет родителя.");
                }
            }
            else
            {
                Debug.LogWarning("Объект, вызвавший триггер, не существует.");
            }
        }
    }
}
