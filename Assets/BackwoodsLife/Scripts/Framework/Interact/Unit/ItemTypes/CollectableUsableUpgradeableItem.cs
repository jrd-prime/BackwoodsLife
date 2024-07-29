using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.ItemTypes
{
    public class CollectableUsableUpgradeableItem : WorldItem, ICollectable, IUsable, IUpgradeable
    {
        [SerializeField] protected SWorldCollectableItemConfig collectableItemConfig;
        [SerializeField] protected SWorldUsableItemConfig usableItemConfig;
        [SerializeField] protected SWorldUpgradeableItemConfig upgradeableItemConfig;

        public override void Process()
        {
            Collect(collectableItemConfig);
            throw new NotImplementedException();
        }

        public void Collect(SWorldCollectableItemConfig itemConfig)
        {
            throw new NotImplementedException();
        }
    }
}
