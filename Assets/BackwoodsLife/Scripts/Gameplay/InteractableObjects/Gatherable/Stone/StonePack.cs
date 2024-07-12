using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.Gatherable.Stone
{
    public class StonePack : InteractableResource
    {
        // public override EInteractableObjectType ResourceType { get; protected set; } = EResourceType.Stone;

        public void OnInteract()
        {
            Debug.LogWarning($"{interactableObjectConfig.upgardable}");

            Debug.Log("I'm StoneStack!");
        }

        public override EInteractableObjectType ResourceType { get; protected set; }
    }
}
