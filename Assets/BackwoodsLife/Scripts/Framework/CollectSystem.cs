using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Gameplay.Environment;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework
{
    public class CollectSystem
    {
        [Inject]
        private void Construct(InventoryManager inventoryManager)
        {
            Debug.LogWarning($"inventoryManager:{inventoryManager}");
        }

        public void Collect(SInteractableCollectableData obj)
        {
            // Debug.LogWarning($"collectRange:{collectRange}");
            var amount = RandomCollector.GetRandom(obj.CollectRange.min, obj.CollectRange.max);

            Debug.LogWarning($"collect  {amount} for {obj.ResourceType}");
            // inventoryManager.AddResource(obj.ResourceType, amount);
        }
    }
}
