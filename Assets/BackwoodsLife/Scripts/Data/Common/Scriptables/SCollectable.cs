using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Framework.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptables
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

        [Title("Collectable Details")] public List<ReturnCollectables> returnedCollectables;
        [Title("Requirements Details")] public List<Requirement> requirementsForCollecting;

        private void OnValidate()
        {
            interactableType = EInteractableObject.Collectable;
            hasRequirements = requirementsForCollecting.Any(JUtils.CheckStructWithListsForItems);
            hasCollectable = returnedCollectables.Any(JUtils.CheckStructWithListsForItems);
        }
    }
}
