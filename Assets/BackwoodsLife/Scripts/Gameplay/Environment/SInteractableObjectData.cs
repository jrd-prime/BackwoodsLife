using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    public abstract class SInteractableObjectData : ScriptableObject
    {
        public abstract EInteractableObjectType InteractableType { get; protected set; }
    }
}
