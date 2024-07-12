using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewResource : NewInteractable
    {
        public override Enum InteractableType { get; protected set; } = EInteractableObjectType.Resource;
        public abstract EResourceType ResourceType { get; protected set; }
    }
}
