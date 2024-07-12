using System;
using BackwoodsLife.Scripts.Framework.Manager.Input;
using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected SInteractableObjectConfig interactableObjectConfig;

        
        public int CollectMin => interactableObjectConfig.collectRange.min;
        public int CollectMax => interactableObjectConfig.collectRange.max;

        public CollectRange CollectRange => interactableObjectConfig.collectRange;
        public bool HasRequirements => interactableObjectConfig.hasRequirements;

        public bool Collectable => interactableObjectConfig.canBeCollected;

        [Inject]
        private void Construct(IInput input)
        {
            Debug.LogWarning($"input:{input}");
            Debug.LogWarning($"Collect:{CollectMin}-{CollectMax}");
        }


        private void OnValidate()
        {
            Assert.IsNotNull(interactableObjectConfig,
                $"You must add the configuration for this prefab : {name}");
        }
    }
}
