using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using R3;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Inventory
{
    public class InventoryViewModel : IViewModel
    {
        private InventoryModel _model;
        public ReadOnlyReactiveProperty<List<InventoryElement>> inventoryDataChanged => _model.OnInventoryChanged;


        [Inject]
        private void Construct(InventoryModel model)
        {
            _model = model;
        }

        public void Initialize()
        {
        }
    }
}
