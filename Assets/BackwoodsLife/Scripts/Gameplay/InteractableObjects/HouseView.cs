using BackwoodsLife.Scripts.Data.InteractableObjectsData;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects
{
    public class HouseView : Interactable
    {
        public override void OnInteract()
        {
            Debug.LogWarning("You touch my house");
        }
    }
}
