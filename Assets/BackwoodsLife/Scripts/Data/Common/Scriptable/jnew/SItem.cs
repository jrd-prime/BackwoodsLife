using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.jnew
{
    public abstract class SItem : ScriptableObject
    {
        [ReadOnly] public string itemName;
        [Title("Main")] [ReadOnly] public EInteractableObject interactableType;
        public EItem itemType;
    }
}
