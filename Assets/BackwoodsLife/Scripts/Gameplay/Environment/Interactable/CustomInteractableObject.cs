using BackwoodsLife.Scripts.Framework.Systems;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] public SInteractableObject data;
        public abstract void Process(IInteractableSystem interactableSystem);
    }

    public abstract class CustomInteractableObject<TScriptableType> : InteractableObject
        where TScriptableType : SInteractableObject
    {
        public TScriptableType localData => data as TScriptableType;
    }
}
