using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    /// <summary>
    /// Предмет который находится в интерфейсе или нужен при взимодействии с скриптами
    /// </summary>
    public abstract class GameItemSettings : ItemSettings
    {
        [Title("Game Item Config")] [ReadOnly] public GameItemType gameItemType;

        [FormerlySerializedAs("itemStorage")] [Title("Item Storage")] public StorageItemType itemType;

        [ShowIf("@itemStorage == EItemStorage.Warehouse")]
        public WarehouseItemSetting warehouseItemSetting;

        [ShowIf("@itemStorage == EItemStorage.Inventory")]
        public InventoryItemSetting inventoryItemSetting;


        protected override void OnValidate()
        {
            base.OnValidate();

            Assert.IsTrue(itemType != StorageItemType.YouMustChoose,
                $"Item storage must be set. Game item config: {name}");

            if (itemType == StorageItemType.Warehouse)
            {
            }
            else if (itemType == StorageItemType.Inventory)
            {
            }
        }
    }


    [Serializable]
    public struct InventoryItemSetting
    {
    }

    [Serializable]
    public struct WarehouseItemSetting
    {
        [Range(1, 1000)] public int stackSize;
    }
}
