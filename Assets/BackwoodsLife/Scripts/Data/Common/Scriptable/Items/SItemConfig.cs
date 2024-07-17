using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items
{
    public abstract class SItemConfig : ScriptableObject
    {
        [ReadOnly] public string itemName;

        [Title("UI Config")] public Sprite uiIcon;
        [Title("Main")] [ReadOnly] public EInteractableObject interactableType;

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
