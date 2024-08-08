using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.System;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class UpgradeItem : CustomWorldInteractableItem<SUpgradeOnlyItem, UpgradeSystem, EUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Upgrade;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<ItemData>> callbackToInteractSystem)
        {
            base.Process(configManager, interactableSystem, callbackToInteractSystem);
        }
    }
}
