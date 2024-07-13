using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    public abstract class SInteractableObjectData : ScriptableObject
    {
        public abstract EInteractableObjectType InteractableType { get; protected set; }
    }
}
