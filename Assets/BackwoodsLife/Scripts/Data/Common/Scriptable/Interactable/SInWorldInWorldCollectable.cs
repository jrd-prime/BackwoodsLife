using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Helpers.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Collectable", order = 1)]
    public class SInWorldInWorldCollectable : SInWorldInteractable
    {
        /// <summary>
        /// At least one requirement? Automatically set onValidate
        /// </summary>
        [ReadOnly] public bool hasRequirements;

        /// <summary>
        /// Return at least one collectable? Automatically set onValidate
        /// </summary>
        [FormerlySerializedAs("hasCollectable")] [ReadOnly]
        public bool isReturnCollectables;

        [ReadOnly] public List<CollectableElement> returnElements;
        [ReadOnly] public List<RequiredElement> requiredElements;

        [Title("Collectable Details")] public List<ReturnCollectable> returnedCollectables;
        [Title("Requirements Details")] public List<RequiredElementForCollect> requirementsForCollecting;

        private bool HasCollectables()
        {
            return true;
            // return returnedCollectables.Any(x => x. != 0);
        }

        // private bool HasRequirements()
        // {
        //     return requirementsForCollecting.Any(x=> x.)
        // }

        private void OnValidate()
        {
            interactableType = EInteractableObject.Collectable;
            hasRequirements = false; //  HasRequirements();
            isReturnCollectables = HasCollectables();

            if (isReturnCollectables)
            {
                returnElements = new List<CollectableElement>();

                foreach (var collectable in returnedCollectables)
                {
                    returnElements.Add(new CollectableElement
                        { Name = collectable.item.name, Range = collectable.range });
                }
            }
            else
            {
                returnElements.Clear();
            }

            if (hasRequirements)
            {
                // JStructHelper.CompileRequiredElements(ref requiredElements, ref requirementsForCollecting);
            }
            else
            {
                requiredElements.Clear();
            }
        }
    }

    [Serializable]
    public struct RequiredElementForCollect
    {
        public IRequired Required;
        public int value;
    }

    public interface IRequired
    {
    }
}
