using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    [CreateAssetMenu(fileName = "name",
        menuName = "Backwoods Life Scripts/Interactables/Objects/New Usable", order = 1)]
    public class SUsable : SInteractableObject
    {
        private void OnValidate()
        {
            interactableType = EInteractableObject.Usable;
        }
    }
}
