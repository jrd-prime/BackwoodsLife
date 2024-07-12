using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewUsable : NewInteractable
    {
        public override Enum InteractableType { get; protected set; } = EInteractableObjectType.Usable;
        public abstract EBuildingType BuildingType { get; protected set; }

    }
}
