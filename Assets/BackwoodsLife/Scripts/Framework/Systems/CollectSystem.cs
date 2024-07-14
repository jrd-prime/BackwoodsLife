using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Gameplay.NewLook.Scriptables;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Systems
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
