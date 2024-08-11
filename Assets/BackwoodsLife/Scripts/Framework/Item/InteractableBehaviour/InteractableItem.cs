using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.System;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Scope;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour
{
    public abstract class InteractableItem<TItemConfig, TInteractSystem, TEnumItemName> : InteractableItemBase
        where TItemConfig : SWorldItemConfig
        where TEnumItemName : Enum
        where TInteractSystem : class, IBaseSystem
    {
        [SerializeField] public TEnumItemName itemName;

        protected IObjectResolver Container;
        protected IConfigManager ConfigManager;
        protected TItemConfig WorldItemConfig;
        protected TInteractSystem CurrentInteractableSystem;
        protected IPlayerViewModel PlayerViewModel;
        protected CharacterOverUI CharacterOverUIHolder;
        protected InteractItemInfoPanelUI InteractItemInfoPanelUI;
        protected GameDataManager GameDataManager;


        protected Action<IInteractZoneState> InteractZoneCallback;


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

            WorldItemConfig = ConfigManager.GetItemConfig<TItemConfig>(itemName.ToString());
            Assert.IsNotNull(WorldItemConfig, $"{itemName}. Config not set");

            PlayerViewModel = Container.Resolve<IPlayerViewModel>();
            Assert.IsNotNull(PlayerViewModel, "PlayerViewModel is null");

            CharacterOverUIHolder = Container.Resolve<CharacterOverUI>();
            Assert.IsNotNull(CharacterOverUIHolder, "CharacterOverUIHolder is null");

            GameDataManager = Container.Resolve<GameDataManager>();
            Assert.IsNotNull(GameDataManager, "GameDataManager is null");

            InteractItemInfoPanelUI = Container.Resolve<InteractItemInfoPanelUI>();
            Assert.IsNotNull(InteractItemInfoPanelUI, "InteractItemInfoPanelUI is null");
        }
    }
}
