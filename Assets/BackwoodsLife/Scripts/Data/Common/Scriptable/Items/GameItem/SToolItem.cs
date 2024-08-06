﻿using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
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
