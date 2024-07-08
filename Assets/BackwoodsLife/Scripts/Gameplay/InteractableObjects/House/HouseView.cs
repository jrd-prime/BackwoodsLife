using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.House
{
    public class HouseView : Interactable
    {
        public override void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
