using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class CollectableItem : CustomWorldInteractableItem<SCollectOnlyItem, CollectSystem, ECollectName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Collect;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractAnimation> callback)
        {
            base.Process(configManager, interactableSystem, callback);

            if (!WorldItemConfig.HasCollectables())
            {
                Debug.LogWarning($"{WorldItemConfig.itemName} has no collectables");
                return;
            }

            if (WorldItemConfig.HasRequirements())
            {
                Debug.LogWarning($"{WorldItemConfig.itemName}  has collectables, has requirements");
            }
            else
            {
                Debug.LogWarning($"{WorldItemConfig.itemName} has collectables, no requirements, just collect");

                var list = new List<InventoryElement>();

                foreach (var returnedItem in WorldItemConfig.collectConfig.returnedItems)
                {
                    list.Add(new InventoryElement()
                    {
                        typeName = returnedItem.item.itemName,
                        Amount = RandomCollector.GetRandom(returnedItem.range.min, returnedItem.range.max)
                    });
                }

                callback.Invoke(list, WorldItemConfig.interactAnimation);
                this.CurrentInteractableSystem.Collect(ref list);
            }
        }
    }
}
