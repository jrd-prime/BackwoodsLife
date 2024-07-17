using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(fileName = "GameItemsList", menuName = SOPathName.ItemsConfigPath + "Game Items list config",
        order = 1)]
    public class SGameItemsList : ScriptableObject
    {
        public List<SGameItemConfig> GameItems;
    }
}
