using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Requriments;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Usable And Upgradable", order = 1)]
    public class SUsableAndUpgradable : SInteractableObject
    {
        [Title("Levels Details (default lvl = 0)")]
        public List<UpgradeLevel> upgradeLevels;

        private void OnValidate()
        {
            interactableType = EInteractableObject.UsableAndUpgradable;
        }
    }

    [Serializable]
    public struct UpgradeLevel
    {
        public int level;
        public List<Requirement> requirementsForUpgrading;
    }
}
