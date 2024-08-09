using BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System
{
    public abstract class ItemSystem
    {
        protected WarehouseManager WarehouseManager;

        [Inject]
        private void Construct(WarehouseManager warehouseManager) => WarehouseManager = warehouseManager;
    }
}
