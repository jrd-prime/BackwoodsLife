using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Enums;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Collectable", order = 1)]
    public class SCollectable : SInteractableObject
    {
        [ReadOnly] public bool hasRequirements;
        [ReadOnly] public bool hasCollectable;
        [Title("Collectable Details")] public List<ReturnCollectables> returnedCollectables;
        [Title("Requirements Details")] public List<Requirement> requirementsForCollecting;

        private void OnValidate()
        {
            interactableType = EInteractableObject.Collectable;
            hasRequirements = HasRequirements();
            hasCollectable = HasCollectable();
        }

        private bool HasRequirements() => requirementsForCollecting.Any(JUtils.CheckStructWithListsForItems);
        private bool HasCollectable() => returnedCollectables.Any(JUtils.CheckStructWithListsForItems);
    }

    [Serializable]
    public struct CollectRange
    {
        public int min;
        public int max;
    }

    [Serializable]
    public struct ReturnCollectables
    {
        public List<ReturnCollectableCustom<EResource>> returnResorces;
        public List<ReturnCollectableCustom<EFood>> returnFood;
    }

    [Serializable]
    public struct ReturnCollectableCustom<T> where T : Enum
    {
        public T collectableType;
        public CollectRange collectRange;
    }
}
