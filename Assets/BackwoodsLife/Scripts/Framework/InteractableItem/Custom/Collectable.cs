using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.System.Item;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.InteractableItem.Custom
{
    /// <summary>
    /// Предмет, который находится в игровом мире и который можно собрать. Растения, руда, деревья и т.д.
    /// </summary>
    public abstract class Collectable : InteractableItem<SCollectableItem, Collect, ECollectName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Collect;

        public override void Process(Action<List<ItemData>> interactSystemCallback)
        {
            Assert.IsNotNull(interactSystemCallback, "interactSystemCallback is null");

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

                var systemResult = CurrentInteractableSystem.Process(processedItems);

                if (systemResult)
                    interactSystemCallback.Invoke(processedItems);
                else
                    Debug.LogError($"{CurrentInteractableSystem.GetType()} failed to process items");
            }
        }
    }
}
