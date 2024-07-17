using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(fileName = "WorldItemsList", menuName = SOPathName.ItemsConfigPath + "World Items list config",
        order = 1)]
    public class SWorldItemsList : ScriptableObject
    {
        public List<SWorldItemsConfig> itemssss;
    }
}
