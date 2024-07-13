using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Framework.Scriptable;
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
        
        public void Spend(ref SInteractableObjectConfig obj)
        {
            // Debug.LogWarning("SPEND");
            // _inventoryManager.DecreaseResource(obj.resourceType, amount);
            //
            // _inventoryManager.DecreaseResources(new List<InventoryElement>
            // {
            //     new() { Type = obj.resourceType, Amount = 33 },
            //     new() { Type = EResourceType.Wood, Amount = 22 }
            // });
        }
    }
}
