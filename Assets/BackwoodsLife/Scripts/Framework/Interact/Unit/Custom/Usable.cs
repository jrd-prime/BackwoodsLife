using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.System;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class Usable : CustomInteractableObject<SInWorldInWorldUsable>
    {
        public override void Process(IInteractableSystem interactableSystem, Action<List<InventoryElement>> callback)
        {
            throw new NotImplementedException();
        }
    }
}
