using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Const;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "ToolItem",
        menuName = SOPathName.GameItemPath + "Tool Item",
        order = 1)]
    public class SToolItem : SCraftableItem<SToolItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            
        }

        private void Awake()
        {
            gameItemType = EGameItemType.Tool;
        }
    }
}
