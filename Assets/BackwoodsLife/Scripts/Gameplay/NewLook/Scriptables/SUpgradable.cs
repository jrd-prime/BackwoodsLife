using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.NewLook.Scriptables
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Upgradable", order = 1)]
    public class SUpgradable : SInteractableObject
    {
        private void OnValidate()
        {
            interactableType = EInteractableObject.Upgradable;
        }
    }
}
