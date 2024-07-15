using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using UnityEngine;
using UnityEngine.Assertions;
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

        public void Collect(ref List<CollectableElement> obj)
        {
            Debug.LogWarning("COLLECT");
            Assert.IsNotNull(_inventoryManager, "inventoryManager is null");
            Assert.IsNotNull(obj, "obj is null");
            var list = obj.Select(element => new InventoryElement
                {
                    Type = element.Name,
                    Amount = RandomCollector.GetRandom(element.Range.min, element.Range.max)
                })
                .ToList();


            _inventoryManager.IncreaseResource(list);
        }
    }
}
