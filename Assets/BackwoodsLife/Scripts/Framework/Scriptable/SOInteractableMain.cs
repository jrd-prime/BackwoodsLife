using System;
using System.Collections.Generic;
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

    [CreateAssetMenu(fileName = "InteractableMainObjectName", menuName = "BLScriptable/MainInteractableObject",
        order = 100)]
    public class SOInteractableMain : ScriptableObject
    {
        [ReadOnly] public string Name;
        public WorldPosition worldPosition = WorldPosition.NotSet;
        public InteractableObjectType interactableObjectType = InteractableObjectType.NotSet;
        public bool Upgardable;

        [ShowIf("@this.Upgardable == true")] [Range(1, 10)]
        public int MaxLevel;

        [ShowIf("@this.Upgardable == true")] public List<SOInteractable> Lelvels;

        private void OnValidate()
        {
            Name = name;
            Assert.AreNotEqual(worldPosition, WorldPosition.NotSet,
                $"WorldPosition must be set. ScriptableObject: {name}");
            Assert.AreNotEqual(interactableObjectType, InteractableObjectType.NotSet,
                $"InteractableObjectType must be set. ScriptableObject: {name}");
        }
    }
}
