using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem
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
        }

        private void Awake()
        {
            gameItemType = EGameItemType.Skill;
        }
    }
}
