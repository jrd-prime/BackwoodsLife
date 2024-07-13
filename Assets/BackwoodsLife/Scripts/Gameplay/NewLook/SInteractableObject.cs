using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    public abstract class SInteractableObject : ScriptableObject
    {
        public EInteractableObject interactableType;

        [Title("Default prefab used like level 0")]
        public AssetReferenceT<GameObject> levelZeroPrefab;
    }
}
