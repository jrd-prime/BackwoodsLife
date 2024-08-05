using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "ResourceItem",
        menuName = SOPathName.GameItemPath + "Resource Item",
        order = 1)]
    public class SResourceItem : SGameItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();

            gameItemType = EGameItem.Resource;
        }
    }
}
