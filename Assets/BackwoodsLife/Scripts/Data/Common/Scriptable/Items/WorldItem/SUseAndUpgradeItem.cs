using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem
{
    /// <summary>
    /// То что мы можем как-либо использовать и улучгать. Например, костер (готовить еду, улучшить костер)
    /// </summary>
    [CreateAssetMenu(
        fileName = "UseAndUpgradeItem",
        menuName = SOPathName.WorldItemPath + "Use And Upgrade Item",
        order = 1)]
    public class SUseAndUpgradeItem : SWorldItemConfig
    {
    }
}
