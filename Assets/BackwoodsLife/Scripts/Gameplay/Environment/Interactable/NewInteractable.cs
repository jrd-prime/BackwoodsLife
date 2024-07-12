using System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class NewInteractable : MonoBehaviour
    {
        public abstract Enum InteractableType { get; protected set; }
        [SerializeField] public SInteractableObjectData data;
    }
}
