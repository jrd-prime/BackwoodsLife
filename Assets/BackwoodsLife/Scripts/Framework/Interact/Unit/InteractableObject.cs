using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] public SInteractableObject data;
        public abstract void Process(IInteractableSystem interactableSystem, Action<List<InventoryElement>> callback);
    }

}
