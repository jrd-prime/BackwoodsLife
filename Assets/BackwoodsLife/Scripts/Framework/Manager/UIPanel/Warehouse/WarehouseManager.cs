using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse
{
    public class WarehouseManager : IInitializable
    {
        private WarehouseDataModel _model;
        private UIButtonsController _uiButtonsController;
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(WarehouseDataModel model, UIButtonsController uiButtonsController)
        {
            _model = model;
            _uiButtonsController = uiButtonsController;
        }

        public void Initialize()
        {
            Assert.IsNotNull(_model, "Warehouse data model is null");
            Assert.IsNotNull(_uiButtonsController, "UiButtonsController is null");

            _model.SetInitializedInventory(InitItems());
            _uiButtonsController.WarehouseButtonClicked
                .Subscribe(_ => { WarehouseButtonClicked(); })
                .AddTo(_disposable);
        }

        public Dictionary<string, int> GetInventoryData() => InitItems();

        private void WarehouseButtonClicked()
        {
            Debug.LogWarning("Warehouse clicked");
        }

        private Dictionary<string, int> InitItems()
        {
            return (
                    from type in new List<Type> { typeof(EResource), typeof(EFood) }
                    from name in Enum.GetNames(type)
                    where name != "None"
                    select name)
                .ToDictionary(name => name, name => 0);
        }

        public void IncreaseResource(string res, int amount) =>
            _model.IncreaseResource(res, amount);

        public void IncreaseResource(in List<InventoryElement> inventoryElements) =>
            _model.IncreaseResource(inventoryElements);

        public void DecreaseResource(string objResourceType, int amount)
        {
            if (_model.HasEnoughResource(objResourceType, amount))
                _model.DecreaseResource(objResourceType, amount);
            else
                Debug.LogWarning($"Not enough {objResourceType}");
        }

        public void DecreaseResources(List<InventoryElement> inventoryElements)
        {
            if (_model.HasEnoughResource(inventoryElements))
                _model.DecreaseResource(inventoryElements);
            else
                Debug.LogWarning($"Not enough {inventoryElements}");
        }
    }
}
