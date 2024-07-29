using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem.UsableAndUpgradable;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class UsableAndUpgradable : CustomWorldInteractableItem<SUsableAndUpgradableItem>
    {
        //     public override EWorldItem worldItemType { get; protected set; }
        //
        //     public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
        //         Action<List<InventoryElement>> callback)
        //     {
        //         throw new NotImplementedException();
        //     }
        public override EWorldItem worldItemType { get; protected set; }

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractType> callback)
        {
            throw new NotImplementedException();
        }
    }
}
