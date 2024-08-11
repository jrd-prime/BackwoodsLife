using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour
{
    public abstract class InteractableItemBase : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }
        public abstract void Process(Action<IInteractZoneState> interactZoneCallback);
    }
}
