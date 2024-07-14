using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptables
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
