using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Inventory
{
    public class InventoryManager : ILoadingOperation
    {
        private InventoryModel _model;
        public string Description => "Inventory Manager";

        [Inject]
        private void Construct(InventoryModel model)
        {
            _model = model;
        }

        public void ServiceInitialization()
        {
        }

        public void IncreaseResource(Enum res, int amount) =>
            _model.IncreaseResource(res, amount);

        public void IncreaseResource(List<InventoryElement> inventoryElements) =>
            _model.IncreaseResource(inventoryElements);

        public void DecreaseResource(Enum objResourceType, int amount)
        {
            if (_model.HasEnoughResource(objResourceType, amount))
            {
                _model.DecreaseResource(objResourceType, amount);
            }
            else
            {
                Debug.LogWarning($"Not enough {objResourceType}");
            }
        }

        public void DecreaseResources(List<InventoryElement> inventoryElements)
        {
            if (_model.HasEnoughResource(inventoryElements))
            {
                _model.DecreaseResource(inventoryElements);
            }
            else
            {
                Debug.LogWarning($"Not enough {inventoryElements}");
            }
        }
    }
}
