using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Collectable Data",
        order = 100)]
    public class SInteractableCollectableData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EResourceType ResourceType;
        public CollectRange CollectRange;
    }
}
