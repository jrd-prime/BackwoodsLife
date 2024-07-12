using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewCollectable : NewInteractable
    {
        public override Enum InteractableType { get; protected set; } = EInteractableObjectType.Collectable;
        public abstract EResourceType ResourceType { get; protected set; }
    }
}
