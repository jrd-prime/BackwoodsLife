using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse
{
    public abstract class SWarehouseItem : SGameItemConfig
    {
        [Title("Warehouse")] [Range(1, 1000)] public int maxStackSize = 1;
    }
}
