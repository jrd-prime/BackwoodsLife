using BackwoodsLife.Scripts.Data.Enums;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/New Usable Data",
        order = 100)]
    public class SInteractableUsableData : SInteractableObjectData
    {
        public override EInteractableObjectType InteractableType { get; protected set; }
        public EBuilding building { get; protected set; }
    }
}
