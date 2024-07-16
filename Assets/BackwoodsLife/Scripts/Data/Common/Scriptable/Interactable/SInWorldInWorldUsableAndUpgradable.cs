using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Usable And Upgradable", order = 1)]
    public class SInWorldInWorldUsableAndUpgradable : SInWorldInteractable
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
        public List<RequirementForCollect> requirementsForUpgrading;
    }
}
