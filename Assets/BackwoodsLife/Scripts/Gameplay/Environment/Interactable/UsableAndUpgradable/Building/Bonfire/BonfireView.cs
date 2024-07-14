using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable.Building.Bonfire
{
    public class BonfireView : Framework.Interact.Unit.Custom.UsableAndUpgradable
    {
        public void OnInteract()
        {
            Debug.LogWarning("<color=red>Bonfire interacted!</color>");
        }
    }
}
