using System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects
{
    public class TriggerZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что объект, вошедший в зону триггера, находится на слое Player
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // Действие, которое произойдет при входе игрока в зону триггера
                Debug.Log("Игрок вошел в зону триггера!");
                // Добавьте здесь другие действия, которые вы хотите выполнить

                // Проверяем, что other не null
                if (other != null)
                {
                    // Получаем родительский объект текущего объекта
                    Transform parentTransform = transform.parent;

                    if (parentTransform != null)
                    {
                        Debug.LogWarning(transform.parent.name);
                        var intt = transform.parent.GetComponent<Interactable>();

                        if (intt == null)
                        {
                            throw new NullReferenceException(
                                $"Interactable is null: {transform.parent.name}. You must set to object Interactable component. ");
                        }

                        intt.OnInteract();
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
}
