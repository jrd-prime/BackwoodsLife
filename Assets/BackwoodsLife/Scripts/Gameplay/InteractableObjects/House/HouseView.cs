using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.House
{
    public class HouseView : Interactable
    {
        public  void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
