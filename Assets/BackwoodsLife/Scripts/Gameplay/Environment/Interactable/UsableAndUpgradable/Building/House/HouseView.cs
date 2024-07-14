using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable.Building.House
{
    public class HouseView : Framework.Interact.Unit.Custom.UsableAndUpgradable
    {
        public  void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
