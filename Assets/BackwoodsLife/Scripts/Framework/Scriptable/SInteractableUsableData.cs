using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Usable Data",
        order = 100)]
    public class SInteractableUsableData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EBuildingType BuildingType { get; protected set; }
    }
}
