using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable
{
    public class House : WorldInteractableItem
    {
        public override EWorldItemType worldItemTypeType { get; protected set; }
        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem, Action<List<InventoryElement>, EInteractAnimation> callback)
        {
            throw new NotImplementedException();
        }
    }
}
