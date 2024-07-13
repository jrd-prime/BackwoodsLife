using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Collectable", order = 1)]
    public class SCollectable : SInteractableObject
    {
        [Title("Collectable Details")] public List<ReturnedCollectable> returnedCollectables;

        [Title("Requirements Details")] public List<Requirement> requirementsForCollecting;

        private void OnValidate()
        {
            interactableType = EInteractableObject.Collectable;
        }
    }

    [Serializable]
    public struct CollectRange
    {
        public int min;
        public int max;
    }

    [Serializable]
    public struct ReturnedCollectable
    {
         public EResource resourceType;
        public CollectRange collectRange;
    }

    public enum EResource
    {
        None,
        Stick,
        Wood,
        Stone
    }
}
