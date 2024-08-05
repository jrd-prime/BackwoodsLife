using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items
{
    public abstract class SItemConfig : ScriptableObject
    {
        [ReadOnly] public string itemName;

        [Title("UI Config")] public AssetReferenceTexture2D icon;
        // [Title("Main")] [ReadOnly] public EInteractableObject interactableType;

        protected virtual void OnValidate()
        {
            itemName = name;
            // interactableType = EInteractableObject.Collectable;
        }
    }

    public abstract class SGameItemConfig : SItemConfig
    {
        [ReadOnly] public EGameItem gameItemType;
    }

    public abstract class SWorldItemConfig : SItemConfig
    {
        public EWorldItem worldItemType;
        public EInteractType interactType;
    }

    public abstract class SWarehouseItem : SGameItemConfig
    {
        [Title("Warehouse")] [Range(1, 1000)] public int maxStackSize = 1;
    }
}
