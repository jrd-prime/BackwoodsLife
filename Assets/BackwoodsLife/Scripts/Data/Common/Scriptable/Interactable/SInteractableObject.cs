using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable
{
    public abstract class SInteractableObject : ScriptableObject
    {
        [ReadOnly] public EInteractableObject interactableType;

        [Title("Default prefab used like level 0")]
        public AssetReferenceT<GameObject> levelZeroPrefab;

    }
}
