using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;

namespace BackwoodsLife.Scripts.Framework.Item.System
{
    /// <summary>
    /// Система, которая подразумевает какие-либо действия над предметами (Ресурс, еда, и т.д.)
    /// </summary>
    public interface IItemSystem : IBaseSystem
    {
        public bool Process(List<ItemDto> itemsData);
    }
}
