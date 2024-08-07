using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit
{
    public abstract class
        CustomWorldInteractableItem<TSWorldItemConfigType, TInteractSystem, TEnumItemName> : WorldInteractableItem
        where TSWorldItemConfigType : SWorldItemConfig
        where TEnumItemName : Enum
        where TInteractSystem : class, IInteractableSystem
    {
        [SerializeField] public TEnumItemName itemName;
        protected TSWorldItemConfigType WorldItemConfig;
        protected TInteractSystem CurrentInteractableSystem;

        public override void Process(IConfigManager configManager, IInteractableSystem interactableSystem,
            Action<List<InventoryElement>> callbackToInteractSystem)
        {
            Assert.IsNotNull(configManager, "configManager is null");
            Assert.IsNotNull(interactableSystem, "interactableSystem is null");

            CurrentInteractableSystem = interactableSystem as TInteractSystem;

            WorldItemConfig = configManager.GetItemConfig<TSWorldItemConfigType>(itemName.ToString());
            Assert.IsNotNull(WorldItemConfig, $"{this}. Config not set");

            if (WorldItemConfig.interactAnimation == EInteractAnimation.NotSet)
                throw new NullReferenceException("Interact animation is null");
        }
    }
}
