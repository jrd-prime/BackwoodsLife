using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Upgradable", order = 1)]
    public class SInWorldInWorldUpgradable : SInWorldInteractable
    {
        private void OnValidate()
        {
            interactableType = EInteractableObject.Upgradable;
        }
    }
}
