using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Enums;
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
            // TODO load and initialize inventory data
            _model.SetInitializedInventory(InitItems());
        }

        private Dictionary<string, int> InitItems()
        {
            List<Type> list = new() { typeof(EResource), typeof(EFood) };

            return (
                    from type in list
                    from name in Enum.GetNames(type)
                    where name != "None"
                    select name)
                .ToDictionary(name => name, name => 0);
        }

        public void IncreaseResource(string res, int amount) =>
            _model.IncreaseResource(res, amount);

        public void IncreaseResource(List<InventoryElement> inventoryElements) =>
            _model.IncreaseResource(inventoryElements);

        public void DecreaseResource(string objResourceType, int amount)
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

        public Dictionary<string, int> GetInventoryData() => InitItems();
    }
}
