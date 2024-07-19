using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem.Warehouse;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem.Collectable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Settings;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class CollectableItem : CustomWorldInteractableItem<SCollectableItem>
    {
        [SerializeField] public ECollectable collectableType;

        private CollectSystem _collectSystem;
        public override EWorldItem worldItemType { get; protected set; } = EWorldItem.Collectable;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>> callback)
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
                }
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
