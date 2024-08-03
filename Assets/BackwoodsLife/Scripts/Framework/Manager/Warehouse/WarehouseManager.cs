using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.Warehouse
{
    public class WarehouseManager : IInitializable
    {
        public string Description => "Inventory Manager";

        private WarehouseModel _model;
        private UIButtonsController _uiButtonsController;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(WarehouseModel model, UIButtonsController uiButtonsController)
        {
            _model = model;
            _uiButtonsController = uiButtonsController;
        }

        public void Initialize()
        {
            _model.SetInitializedInventory(InitItems());
            _uiButtonsController.WarehouseButtonClicked
                .Subscribe(_ => { WarehouseButtonClicked(); })
                .AddTo(_disposable);
        }

        private void WarehouseButtonClicked()
        {
            Debug.LogWarning("Warehouse clicked");
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
