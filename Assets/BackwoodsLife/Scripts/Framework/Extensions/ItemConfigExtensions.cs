using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Extensions
{
    public static class ItemConfigExtensions
    {
        public static bool HasCollectables(this CollectableItem itemConfig)
        {
            return itemConfig.collectConfig.returnedItems.Count > 0;
        }

        public static bool HasRequirements(this CollectableItem itemConfig)
        {
            Debug.Log("We check 3 requirements");
            return itemConfig.collectConfig.requiredForCollectDto.building.Count > 0 ||
                   itemConfig.collectConfig.requiredForCollectDto.tool.Count > 0 ||
                   itemConfig.collectConfig.requiredForCollectDto.skill.Count > 0;
        }
    }
}
