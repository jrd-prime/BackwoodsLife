using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Interactable
{
    [CreateAssetMenu(fileName = "Name_main", menuName = "BLScriptable/Interactable/Main Config For New Object",
        order = 100)]
    public class SInteractableObjectMainConfig : ScriptableObject
    {
        [FormerlySerializedAs("name")] [ReadOnly]
        public string objName;


        [FormerlySerializedAs("interactableObjectType")]
        public EInteractableObject eInteractableObjectType = EInteractableObject.Default;

        public Vector3 spawnPosition;
        public bool upgardable;

        [ShowIf("@this.upgardable == true")] [Range(2, 10)]
        public int maxLevel = 2;

        // [ShowIf("@this.upgardable == false")] public SInteractableObjectConfig mainLevel;
        // [ShowIf("@this.upgardable == true")] public List<SInteractableObjectConfig> lelvels;

        private void OnValidate()
        {
            objName = ((Object)this).name;
            Assert.AreNotEqual(eInteractableObjectType, EInteractableObject.Default,
                $"InteractableObjectType must be set. ScriptableObject: {((Object)this).name}");
        }
    }
}
