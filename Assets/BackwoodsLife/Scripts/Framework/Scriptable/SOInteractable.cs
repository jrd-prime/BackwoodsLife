using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    public enum InteractableObjectType
    {
        NotSet,
        Building,
        Gatherable
    }

    public enum WorldPosition
    {
        NotSet,
        Static,
        Custom
    }

    [CreateAssetMenu(fileName = "InteractableObjectName", menuName = "BLScriptable/InteractableObject", order = 100)]
    public class SOInteractable : ScriptableObject
    {
        public AssetReferenceGameObject AssetReference;
        [ReadOnly] public string Name;
        public WorldPosition worldPosition = WorldPosition.NotSet;

        public InteractableObjectType InteractableObjectType = InteractableObjectType.NotSet;
        public bool Upgardable;

        [ShowIf("@this.Upgardable == true")] public int UpgradeLevel = 1;

        private void OnValidate()
        {
            Name = name;
            Assert.IsNotNull(AssetReference, $"Prefab must be set. ScriptableObject: {name}");
            Assert.AreNotEqual(worldPosition, WorldPosition.NotSet,
                $"WorldPosition must be set. ScriptableObject: {name}");
            Assert.AreNotEqual(InteractableObjectType, InteractableObjectType.NotSet,
                $"InteractableObjectType must be set. ScriptableObject: {name}");
        }
    }
}
