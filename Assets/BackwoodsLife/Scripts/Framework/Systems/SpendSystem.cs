using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Systems
{
    public class SpendSystem
    {
        private InventoryManager _inventoryManager;

        [Inject]
        private void Construct(InventoryManager inventoryManager)
        {
            Debug.Log($"inventoryManager:{inventoryManager}");
            _inventoryManager = inventoryManager;
        }
    }
}
