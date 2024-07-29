using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem.UsableAndUpgradable
{
    [CreateAssetMenu(
        fileName = "UsableAndUpgradableItem",
        menuName = SOPathName.WorldItemPath + "Usable And Upgradable Item",
        order = 1)]
    public class SUsableAndUpgradableItem : SWorldItemConfig
    {
        
        public EInteractInterfaceType interactInterfaceType;
    }
}
