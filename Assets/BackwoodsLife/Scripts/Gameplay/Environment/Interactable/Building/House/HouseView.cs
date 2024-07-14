using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Building.House
{
    public class HouseView : UsableAndUpgradable
    {
        public  void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
