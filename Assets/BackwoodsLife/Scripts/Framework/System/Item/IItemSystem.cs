using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.System.Item
{
    /// <summary>
    /// Система, которая подразумевает какие-либо действия над предметами (Ресурс, еда, и т.д.)
    /// </summary>
    public interface IItemSystem : IBaseSystem
    {
        public bool Process(List<ItemData> itemsData);
    }
}
