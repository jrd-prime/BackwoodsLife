using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse
{
    [CreateAssetMenu(
        fileName = "WarehouseItem",
        menuName = SOPathName.GameItemPath + "Warehouse Item",
        order = 1)]
    public class SWarehouseItem : SGameItemConfig
    {
        [Title("Warehouse")] [Range(1, 1000)] public int maxStackSize = 1;
    }
}
