﻿using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Skill
{
    [CreateAssetMenu(
        fileName = "SkillItem",
        menuName = SOPathName.GameItemPath + "Skill Item",
        order = 1)]
    public class SSkillItem : SGameItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            
            gameItemType = EGameItem.Skill;
        }
    }
}
