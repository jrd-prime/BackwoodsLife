using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Gameplay.Environment;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "objectData", menuName = "BLScriptable/Interactable Data/Full object data",
        order = 100)]
    public class SInteractableObjectFullData : ScriptableObject
    {
        public EInteractableObjectType InteractableType = EInteractableObjectType.NotSet;
        
        public SInteractableObjectData data;
    }
}
