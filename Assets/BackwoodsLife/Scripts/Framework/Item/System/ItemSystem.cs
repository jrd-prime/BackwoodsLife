using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.System
{
    public abstract class ItemSystem
    {
        protected WarehouseManager WarehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager) => WarehouseManager = warehouseManager;
    }
}
