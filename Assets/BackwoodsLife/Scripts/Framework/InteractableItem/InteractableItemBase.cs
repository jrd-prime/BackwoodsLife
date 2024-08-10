using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Gameplay.Environment;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.InteractableItem
{
    public abstract class InteractableItemBase : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }
        public abstract void Process(Action<IInteractZoneState> onInteractionFinished);
    }
}
