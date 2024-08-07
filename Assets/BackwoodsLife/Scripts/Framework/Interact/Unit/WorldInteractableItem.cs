using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit
{
    public abstract class WorldInteractableItem : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }

        public abstract void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractAnimation> callbackToInteractSystem);
    }
}
