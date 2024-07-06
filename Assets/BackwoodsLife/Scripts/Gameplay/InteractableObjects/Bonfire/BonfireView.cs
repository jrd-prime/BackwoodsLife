using UnityEngine;

namespace BackwoodsLife.Scripts.Data.InteractableObjectsData
{
    public class BonfireView : Interactable
    {
        public override void OnInteract()
        {
            Debug.LogWarning("<color=red>Bonfire interacted!</color>");
        }
    }
}
