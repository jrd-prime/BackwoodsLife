﻿using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem
{
    /// <summary>
    /// То что мы можем как-либо использовать. Например, колодец
    /// </summary>
    [CreateAssetMenu(
        fileName = "UseOnlyItem",
        menuName = SOPathName.WorldItemPath + "Use Only Item",
        order = 1)]
    public class SUseOnlyItem : SWorldItemConfig
    {
    }
}
