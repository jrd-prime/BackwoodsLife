﻿using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Helpers.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Collectable", order = 1)]
    public class SCollectable : SInteractableObject
    {
        /// <summary>
        /// At least one requirement? Automatically set onValidate
        /// </summary>
        [ReadOnly] public bool hasRequirements;

        /// <summary>
        /// Return at least one collectable? Automatically set onValidate
        /// </summary>
        [ReadOnly] public bool hasCollectable;

        [ReadOnly] public List<CollectableElement> returnableElements;
        [ReadOnly] public List<RequiredElement> requiredElements;

        [Title("Collectable Details")] public List<ReturnCollectables> returnedCollectables;
        [Title("Requirements Details")] public List<RequirementForCollect> requirementsForCollecting;

        private void OnValidate()
        {
            interactableType = EInteractableObject.Collectable;
            hasRequirements = requirementsForCollecting.Any(JUtils.CheckStructWithListsForItems);
            hasCollectable = returnedCollectables.Any(JUtils.CheckStructWithListsForItems);

            if (hasCollectable)
            {
                JStructHelper.CompileReturnableElements(ref returnableElements, ref returnedCollectables);
            }
            else
            {
                returnableElements.Clear();
            }

            if (hasRequirements)
            {
                JStructHelper.CompiledRequiredElements(ref requiredElements, ref requirementsForCollecting);
            }
            else
            {
                requiredElements.Clear();
            }
        }
    }
}
