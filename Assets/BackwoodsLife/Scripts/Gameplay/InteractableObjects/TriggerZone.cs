using System;
using BackwoodsLife.Scripts.Framework;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects
{
    public class TriggerZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что объект, вошедший в зону триггера, находится на слое Player
            if (other.gameObject.layer != (int)JLayers.Player) return;

            Debug.Log($"Игрок вошел в зону триггера! {name}");

            if (other != null)
            {
                Transform parentTransform = transform.parent;

                if (parentTransform != null)
                {
                    Debug.LogWarning(transform.parent.name);
                    var interactable = transform.parent.GetComponent<NewInteractable>();

                    if (interactable == null)
                    {
                        throw new NullReferenceException(
                            $"Interactable is null on {parentTransform.name} prefab. You must set to object Interactable component. ");
                    }

                    var a = other.GetComponent<PlayerInteractSystem>();

                    a.Interact(ref interactable);

                    // _collectSystem.Collect(interactable);
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
