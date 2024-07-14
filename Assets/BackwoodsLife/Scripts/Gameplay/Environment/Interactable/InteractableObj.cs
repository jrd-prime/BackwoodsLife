using BackwoodsLife.Scripts.Framework.Scriptable;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class InteractableObj : MonoBehaviour
    {
        [SerializeField] public SInteractableObject data;
    }
}
