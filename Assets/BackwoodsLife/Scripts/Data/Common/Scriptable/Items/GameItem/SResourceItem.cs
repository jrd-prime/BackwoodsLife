using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "ResourceItem",
        menuName = SOPathName.GameItemPath + "Resource Item",
        order = 1)]
    public class SResourceItem : SCraftableItem<SResourceItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();

        }

        private void Awake()
        {
            gameItemType = EGameItemType.Resource;
        }
    }
}
