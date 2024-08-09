using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    /// <summary>
    /// Предмет который находится в интерфейсе или нужен при взимодействии с скриптами
    /// </summary>
    public abstract class SGameItemConfig : SItemConfig
    {
        [Title("Game Item Config")] [ReadOnly] public EGameItemType gameItemType;

        [Title("Item Storage")] public EItemStorage itemStorage;

        [ShowIf("@itemStorage == EItemStorage.Warehouse")]
        public WarehouseItemSetting warehouseItemSetting;

        [ShowIf("@itemStorage == EItemStorage.Inventory")]
        public InventoryItemSetting inventoryItemSetting;


        protected override void OnValidate()
        {
            base.OnValidate();

            Assert.IsTrue(itemStorage != EItemStorage.YouMustChoose,
                $"Item storage must be set. Game item config: {name}");

            if (itemStorage == EItemStorage.Warehouse)
            {
            }
            else if (itemStorage == EItemStorage.Inventory)
            {
            }
        }
    }


    public enum ECraftingType
    {
        Cooking = 0,
        Craft = 1
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

    public enum EItemStorage
    {
        YouMustChoose = 0,
        Warehouse = 1,
        Inventory = 2
    }
}
