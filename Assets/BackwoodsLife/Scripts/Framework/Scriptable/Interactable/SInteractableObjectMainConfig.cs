using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Interactable
{
    public enum EInteractableObjectType
    {
        NotSet,
        Usable,
        Collectable
    }

    public enum WorldPosition
    {
        NotSet,
        Static,
        Custom
    }

    [CreateAssetMenu(fileName = "Name_main", menuName = "BLScriptable/Interactable/Main Config For New Object",
        order = 100)]
    public class SInteractableObjectMainConfig : ScriptableObject
    {
        [ReadOnly] public string name;
        public WorldPosition worldPosition = WorldPosition.NotSet;
        [FormerlySerializedAs("interactableObjectType")] public EInteractableObjectType eInteractableObjectType = EInteractableObjectType.NotSet;
        public Vector3 spawnPosition;
        public bool upgardable;

        [ShowIf("@this.upgardable == true")] [Range(2, 10)]
        public int maxLevel = 2;

        [ShowIf("@this.upgardable == false")] public SInteractableObjectConfig mainLevel;
        [ShowIf("@this.upgardable == true")] public List<SInteractableObjectConfig> lelvels;

        private void OnValidate()
        {
            name = ((Object)this).name;
            Assert.AreNotEqual(eInteractableObjectType, EInteractableObjectType.NotSet,
                $"InteractableObjectType must be set. ScriptableObject: {((Object)this).name}");
        }
    }
}
