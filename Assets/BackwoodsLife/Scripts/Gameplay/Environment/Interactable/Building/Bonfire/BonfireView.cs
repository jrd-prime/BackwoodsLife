using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Building.Bonfire
{
    public class BonfireView : InteractableObj
    {
        public  void OnInteract()
        {
            Debug.LogWarning("<color=red>Bonfire interacted!</color>");
        }
    }
}
