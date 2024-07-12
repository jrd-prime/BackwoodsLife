using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects
{
    public abstract class InteractableResource : Interactable
    {
        public abstract EInteractableObjectType ResourceType { get; protected set; }
    }
}
