using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using R3;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Inventory
{
    public class InventoryViewModel : IViewModel
    {
        private InventoryModel _model;
        private InventoryManager _manager;
        public ReadOnlyReactiveProperty<List<InventoryElement>> inventoryDataChanged => _model.OnInventoryChanged;


        [Inject]
        private void Construct(InventoryModel model, InventoryManager manager)
        {
            _model = model;
            _manager = manager;
        }

        public void Initialize()
        {
        }

        public Dictionary<string, int> GetInventoryData()
        {
           return _manager.GetInventoryData();
        }
    }
}
