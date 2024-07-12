using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.FoodObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewFood : NewInteractable
    {
        public override Enum InteractableType { get; protected set; } = EInteractableObjectType.Food;
        public abstract EFoodType FoodType { get; protected set; }
    }
}
