﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Interact.Unit.Custom;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable
{
    public class House : UseAndUpgradeItem
    {
        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractAnimation> callbackToInteractSystem)
        {
            throw new NotImplementedException();
        }
    }
}
