using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Framework.Scriptable;
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

        public void Collect(SCollectableData obj)
        {
            // Debug.LogWarning($"collectRange:{collectRange}");
            var amount = RandomCollector.GetRandom(obj.collectRange.Min, obj.collectRange.Max);

            // Debug.LogWarning($"collect  {amount} for {obj.resourceType}");
            // inventoryManager.AddResource(obj.ResourceType, amount);
        }
    }
}
