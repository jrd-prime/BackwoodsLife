using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.Bonfire
{
    public class BonfireView : Interactable
    {
        public  void OnInteract()
        {
            Debug.LogWarning("<color=red>Bonfire interacted!</color>");
        }
    }
}
