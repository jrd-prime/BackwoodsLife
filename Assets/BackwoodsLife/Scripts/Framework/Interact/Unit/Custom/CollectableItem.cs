using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
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
    public class CollectableItem : CustomWorldInteractableItem<SCollectableItem>
    {
        [SerializeField] public ECollectable collectableType;
        [SerializeField] private int collectableLevel;

        private CollectSystem _collectSystem;
        public override EWorldItem worldItemType { get; protected set; } = EWorldItem.Collectable;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>, EInteractType> callback)
        {
            Assert.IsNotNull(configManager, "configManager is null");

            config = configManager.GetWorldItemConfig<SCollectableItem>(collectableType.ToString());

            Assert.IsNotNull(interactableSystem, "interactableSystem is null");
            _collectSystem = interactableSystem as CollectSystem;
            Assert.IsNotNull(_collectSystem, "interactableSystem is not CollectSystem");


            if (config.retunedItemsCount > 0)
            {
                Debug.LogWarning("HAS COLLECTABLE");
                if (config.requiredItemsCount > 0)
                {
                    Debug.LogWarning("HAS REQUIREMENTS");
                }
                else
                {
                    Debug.LogWarning("NO REQUIREMENTS just collect");

                    Debug.LogWarning(" AAAAA === " + config.interactType);

                    // var list = localData.returnElements.Select(element => new InventoryElement
                    //     {
                    //         typeName = element.Name,
                    //         Amount = RandomCollector.GetRandom(element.Range.min, element.Range.max)
                    //     })
                    //     .ToList();
                    //
                    // callback.Invoke(list);
                    //
                    // _collectSystem.Collect(ref list);

                    var list = new List<InventoryElement>();
                    var lvl = config.collectableLevels[collectableLevel];
                    foreach (var returnedItem in lvl.returnedItems)
                    {
                        list.Add(new InventoryElement
                        {
                            typeName = returnedItem.Item.itemName,
                            Amount = RandomCollector.GetRandom(returnedItem.Range.min, returnedItem.Range.max)
                        });
                    }


                    callback.Invoke(list, config.interactType);
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
