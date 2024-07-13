using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Building.House
{
    public class HouseView : InteractableObj
    {
        public  void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
