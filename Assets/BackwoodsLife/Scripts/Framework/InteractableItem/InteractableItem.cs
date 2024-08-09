using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Scope;
using BackwoodsLife.Scripts.Framework.System;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.InteractableItem
{
    public abstract class InteractableItem<TSWorldItemConfigType, TInteractSystem, TEnumItemName> : InteractableItemBase
        where TSWorldItemConfigType : SWorldItemConfig
        where TEnumItemName : Enum
        where TInteractSystem : class, IBaseSystem
    {
        [SerializeField] public TEnumItemName itemName;

        protected IObjectResolver Container;
        protected IConfigManager ConfigManager;
        protected TSWorldItemConfigType WorldItemConfig;
        protected TInteractSystem CurrentInteractableSystem;

        private void Awake()
        {
            var rootContext = FindObjectOfType<GameSceneContext>();
            if (rootContext is null) throw new NullReferenceException("Root context is null");

            Container = rootContext.Container;
            Assert.IsNotNull(Container, "Container is null");

            ConfigManager = Container.Resolve<IConfigManager>();
            Assert.IsNotNull(ConfigManager, "Config Manager is null");

            CurrentInteractableSystem = Container.Resolve<TInteractSystem>();
            Assert.IsNotNull(CurrentInteractableSystem, $"{typeof(TInteractSystem)} is null ");

            WorldItemConfig = ConfigManager.GetItemConfig<TSWorldItemConfigType>(itemName.ToString());
            Assert.IsNotNull(WorldItemConfig, $"{itemName}. Config not set");
        }
    }
}
