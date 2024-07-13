using BackwoodsLife.Scripts.Framework.Scriptable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class InteractableObj : MonoBehaviour
    {
        [SerializeField] public SInteractableObjectConfig data;
    }
}
