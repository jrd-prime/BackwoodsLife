using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
{
    public interface IItemDataManager : IInitializable
    {
        public void Increase(string res, int amount);
        public void Increase(in List<ItemData> inventoryElements);
        public void DecreaseResource(string objResourceType, int amount);
        public void DecreaseResource(in List<ItemData> inventoryElements);
    }
}
