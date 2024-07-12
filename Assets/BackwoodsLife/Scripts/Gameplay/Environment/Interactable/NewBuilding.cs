using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewBuilding : NewInteractable
    {
        public override Enum InteractableType { get; protected set; } = EInteractableObjectType.Building;
        public abstract EBuildingType BuildingType { get; protected set; }

    }
}
