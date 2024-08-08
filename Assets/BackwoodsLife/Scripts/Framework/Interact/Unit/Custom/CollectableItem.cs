using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public abstract class CollectableItem : CustomWorldInteractableItem<SCollectOnlyItem, CollectSystem, ECollectName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Collect;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<ItemData>> callbackToInteractSystem)
        {
            base.Process(configManager, interactableSystem, callbackToInteractSystem);

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
                
                var processedItems = new List<ItemData>();

                foreach (var item in WorldItemConfig.collectConfig.returnedItems)
                {
                    var itemAmount = RandomCollector.GetRandom(item.range.min, item.range.max);

                    processedItems.Add(new ItemData { Name = item.item.itemName, Quantity = itemAmount });
                }
                
                
                // CurrentInteractableSystem.Process(CurrentInteractableSystem, processedItems,
                //     callbackToInteractSystem);
            }
        }
    }
}
