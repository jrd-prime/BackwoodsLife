using System;
using BackwoodsLife.Scripts.Data.Inventory.JObjects;
using BackwoodsLife.Scripts.Data.Inventory.JObjects.ToolObjects;
using BackwoodsLife.Scripts.Gameplay.InteractableObjects.Requriments;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "Name_level_1", menuName = "BLScriptable/Interactable/Object Config", order = 100)]
    public class SInteractableObjectConfig : ScriptableObject
    {
        [ReadOnly] public string objName;
        public AssetReferenceGameObject assetReference;

        public bool hasRequirements;

        [Title("Upgradable")] public bool upgardable;

        [ShowIf("@this.upgardable == true")] public int upgradeLevel = 1;

        [Title("Can Be Collected")] public bool canBeCollected;

        public CollectRange collectRange;


        [ShowIf("@this.canBeCollected == true")]
        public Requirement requiredForResourceCollection;


        private void OnValidate()
        {
            objName = name;
            Assert.IsNotNull(assetReference, $"Prefab must be set. ScriptableObject: {name}");

            // TODO check max possible lvl

            foreach (var building in requiredForResourceCollection.building)
            {
                Assert.IsTrue(building.Type != EBuildingType.None,
                    $"Building type must be set or removed. ScriptableObject: {name}");
            }

            foreach (var building in requiredForResourceCollection.skill)
            {
                Assert.IsTrue(building.Type != ESkillType.None,
                    $"Skill type must be set or removed. ScriptableObject: {name}");
            }

            foreach (var building in requiredForResourceCollection.tool)
            {
                Assert.IsTrue(building.Type != EToolType.None,
                    $"Tool type must be set or removed. ScriptableObject: {name}");
            }

            hasRequirements = requiredForResourceCollection.building.Count > 0 ||
                              requiredForResourceCollection.skill.Count > 0 ||
                              requiredForResourceCollection.tool.Count > 0;
        }
    }

    [Serializable]
    public struct CollectRange
    {
        [Range(1, 1)] public int min;

        [Range(2, 100)] public int max;
    }
}
