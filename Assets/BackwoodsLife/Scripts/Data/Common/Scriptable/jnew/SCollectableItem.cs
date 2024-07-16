using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.jnew
{
    [CreateAssetMenu(fileName = "Name",
        menuName = "Backwoods Life Scripts/Item/New COLLECTABLE", order = 1)]
    public class SCollectableItem : SItem
    {
        [Title("UI Config")] public Sprite uiIcon;

        [Title("Inventory/Warehouse")] [Range(1, 1000)]
        public int maxStackSize = 1;

        private void OnValidate()
        {
            itemName = name;
            interactableType = EInteractableObject.Collectable;
            CheckOnNull("icon", uiIcon);
        }

        private void CheckOnNull(string fieldName, object obj)
        {
            if (obj == null)
                Debug.LogError(fieldName.ToUpper() + " is null in " + name.ToUpper() + " config ");
        }
    }
}
