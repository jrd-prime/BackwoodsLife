using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Extensions
{
    public static class ItemConfigExtensions
    {
        public static bool HasCollectables(this SCollectOnlyItem itemConfig)
        {
            return itemConfig.collectConfig.returnedItems.Count > 0;
        }

        public static bool HasRequirements(this SCollectOnlyItem itemConfig)
        {
            Debug.Log("We check 3 requirements");
            return itemConfig.collectConfig.requirementForCollect.building.Count > 0 ||
                   itemConfig.collectConfig.requirementForCollect.tool.Count > 0 ||
                   itemConfig.collectConfig.requirementForCollect.skill.Count > 0;
        }
    }
}
