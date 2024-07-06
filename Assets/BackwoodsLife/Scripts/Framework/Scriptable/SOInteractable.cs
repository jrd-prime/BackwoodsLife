using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "InteractableObjectName", menuName = "BLScriptable/InteractableObject", order = 100)]
    public class SOInteractable : ScriptableObject
    {
        public AssetReferenceGameObject assetReference;
        [ReadOnly] public string Name;
        public bool Upgardable;

        [ShowIf("@this.Upgardable == true")] public int UpgradeLevel = 1;

        private void OnValidate()
        {
            Name = name;
            Assert.IsNotNull(assetReference, $"Prefab must be set. ScriptableObject: {name}");
        }
    }
}
