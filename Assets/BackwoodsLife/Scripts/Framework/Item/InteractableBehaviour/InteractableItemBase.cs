using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour
{
    public abstract class InteractableItemBase : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }

        /// <summary>
        /// Запускает последовательность действий для обработки интерактабл объекта.
        /// Принимает колбэк для смены состояния триггер зоны.
        /// В колбэк передает созданный экземпляр состояния для триггер зоны
        /// </summary>
        /// <param name="interactZoneCallback"></param>
        public abstract void Process(Action<IInteractZoneState> interactZoneCallback);
    }
}
