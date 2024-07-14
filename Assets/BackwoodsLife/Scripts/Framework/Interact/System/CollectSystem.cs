using BackwoodsLife.Scripts.Data.Common.Scriptables;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    public class CollectSystem : IInteractableSystem
    {
        private InventoryManager _inventoryManager;

        [Inject]
        private void Construct(InventoryManager inventoryManager)
        {
            Debug.Log($"inventoryManager:{inventoryManager}");
            _inventoryManager = inventoryManager;
        }

        public void Collect(ref SCollectable obj)
        {
            Debug.LogWarning("COLLECT");
            // _inventoryManager.IncreaseResource(newList);
        }
    }
}
