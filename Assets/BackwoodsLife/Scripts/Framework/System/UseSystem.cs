using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Framework.Module.ItemsData;

namespace BackwoodsLife.Scripts.Framework.System
{
    public class UseSystem : IInteractableSystem
    {
        public void Process(IItemDataManager itemDataManager, List<ItemData> itemsWithConfigToCollect, Action<List<ItemData>> callback)
        {
            throw new NotImplementedException();
        }
    }
}
