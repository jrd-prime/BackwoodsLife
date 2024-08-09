using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.System.Building;

namespace BackwoodsLife.Scripts.Framework.InteractableItem
{
    public class Upgradeable : InteractableItem<SUpgradeOnlyItem, Upgrade, EUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Upgrade;

        public override void Process(Action<List<ItemData>> interactSystemCallback)
        {
        }
    }
}
