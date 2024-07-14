using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Building.Bonfire
{
    public class BonfireView : UsableAndUpgradable
    {
        public void OnInteract()
        {
            Debug.LogWarning("<color=red>Bonfire interacted!</color>");
        }
    }
}
