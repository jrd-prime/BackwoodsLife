using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.FoodObjects;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewInteractable : MonoBehaviour
    {
        public abstract Enum InteractableType { get; protected set; }
        [SerializeField] protected SInteractableObjectData data;
        public bool HasRequirements { get; protected set; } = true;


        private void OnValidate()
        {
            HasRequirements =
        }
    }


    public abstract class SInteractableObjectData : ScriptableObject
    {
        public abstract EInteractableObjectType InteractableType { get; protected set; }
    }


    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Resource Data",
        order = 100)]
    public class SInteractableResourceData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EResourceType ResourceType { get; protected set; }
    }

    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Building Data",
        order = 100)]
    public class SInteractableBuildingData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EBuildingType BuildingType { get; protected set; }
    }

    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Food Data",
        order = 100)]
    public class SInteractableFoodData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EFoodType FoodType { get; protected set; }
    }
}
