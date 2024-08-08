using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit
{
    public abstract class WorldInteractableItem : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }

        public abstract void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<ItemData>> callbackToInteractSystem);
    }
}
