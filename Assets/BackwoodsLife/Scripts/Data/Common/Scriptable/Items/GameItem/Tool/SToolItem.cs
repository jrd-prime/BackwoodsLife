using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Tool
{
    [CreateAssetMenu(
        fileName = "ToolItem",
        menuName = SOPathName.GameItemPath + "Tool Item",
        order = 1)]
    public class SToolItem : SGameItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            
            gameItemType = EGameItem.Tool;
        }
    }
}
