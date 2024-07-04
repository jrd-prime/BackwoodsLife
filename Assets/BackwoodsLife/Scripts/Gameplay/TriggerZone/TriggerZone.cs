using System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.TriggerZone
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
            }
        }
    }
}
