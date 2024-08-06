using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class CollectableItem : CustomWorldInteractableItem<SCollectOnlyItem>
    {
        [SerializeField] public ECollectable collectableType;
        [SerializeField] private int collectableLevel;

        [SerializeField] private SCollectOnlyItem worldItemConfig;

        private CollectSystem _collectSystem;
        public override EWorldItemType worldItemTypeType { get; protected set; } = EWorldItemType.Collectable;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractAnimation> callback)
        {
            Assert.IsNotNull(configManager, "configManager is null");
            Assert.IsNotNull(worldItemConfig, $"{this}. Config not set");

            var itemConfig = configManager.GetItemConfig<SCollectOnlyItem>(worldItemConfig.itemName);

            Assert.IsNotNull(interactableSystem, "interactableSystem is null");
            _collectSystem = interactableSystem as CollectSystem;
            Assert.IsNotNull(_collectSystem, "interactableSystem is not CollectSystem");


            if (itemConfig.interactAnimation == EInteractAnimation.NotSet)
                throw new NullReferenceException("Interact animation is null");

            if (itemConfig.collectConfig.returnedItems.Count > 0)
            {
                Debug.LogWarning("HAS COLLECTABLE");
                if (itemConfig.collectConfig.requirementForCollect.building.Count > 0 ||
                    itemConfig.collectConfig.requirementForCollect.tool.Count > 0 ||
                    itemConfig.collectConfig.requirementForCollect.skill.Count > 0)
                {
                    Debug.LogWarning("HAS REQUIREMENTS");
                }
                else
                {
                    Debug.LogWarning("NO REQUIREMENTS just collect");
                    var list = new List<InventoryElement>();

                    foreach (var returnedItem in itemConfig.collectConfig.returnedItems)
                    {
                        list.Add(new InventoryElement()
                        {
                            typeName = returnedItem.item.itemName,
                            Amount = RandomCollector.GetRandom(returnedItem.range.min, returnedItem.range.max)
                        });
                    }

                    callback.Invoke(list, itemConfig.interactAnimation);
                    _collectSystem.Collect(ref list);
                }
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
