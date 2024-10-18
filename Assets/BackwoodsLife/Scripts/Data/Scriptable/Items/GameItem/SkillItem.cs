using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Const;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.GameItem
{
    [CreateAssetMenu(
        fileName = "SkillItem",
        menuName = SOPathName.GameItemPath + "Skill Item",
        order = 1)]
    public class SkillItem : GameItemSettings
    {
        protected override void OnValidate()
        {
            base.OnValidate();
        }

        private void Awake()
        {
            gameItemType = GameItemType.Skill;
        }
    }
}
