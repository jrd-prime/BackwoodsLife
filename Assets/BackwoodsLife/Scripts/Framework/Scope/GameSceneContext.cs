using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using BackwoodsLife.Scripts.Framework.Item.System;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Framework.Item.System.Item;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanels;
using BackwoodsLife.Scripts.Framework.Manager.UIPanels.BuildingPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanels.Quest;
using BackwoodsLife.Scripts.Gameplay.Environment;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.InteractPanel;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActions;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.Warehouse;
using BackwoodsLife.Scripts.Gameplay.UI.UIButtons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class GameSceneContext : LifetimeScope
    {
        [SerializeField] private UIFrameController uiFrameController;
        [SerializeField] private UIButtonsController uiButtonsController;
        [SerializeField] private CharacterOverUI characterOverUIHolder;
        [SerializeField] private InteractPanelUI interactPanelUIHolder;

        [Title("Panels ")] [SerializeField] private BuildingPanelUIController buildingPanelUIController;

        [FormerlySerializedAs("questPanelUIController")] [SerializeField]
        private QuestPanelUI questPanelUI;

        [SerializeField] private WarehousePanelUIController warehousePanelUIController;
        [SerializeField] private InteractItemInfoPanelUI interactItemInfoPanelUIController;

        [FormerlySerializedAs("useActionsPanelUIController")] [SerializeField]
        private UseActionsPanelUI useActionsPanelUI;


        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("GameSceneContext");

            Assert.IsNotNull(buildingPanelUIController,
                "buildingPanelController is null. Add prefab to scene and set prefab to " + name);
            Assert.IsNotNull(questPanelUI,
                "questPanelController is null. Add prefab to scene and set prefab to " + name);
            Assert.IsNotNull(warehousePanelUIController,
                "warehousePanelController is null. Add prefab to scene and set prefab to " + name);

            builder.Register<PlayerViewModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<JoystickViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WarehouseViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // System
            builder.Register<ItemSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<CollectSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<SpendSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<UseSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<UpgradeSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<UseAndUpgradeSystem>(Lifetime.Singleton).AsSelf();

            builder.Register<WarehouseManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<QuestManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<JoystickModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<BuildingPanelFiller>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterComponent(buildingPanelUIController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(uiFrameController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(uiButtonsController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(characterOverUIHolder).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactPanelUIHolder).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactItemInfoPanelUIController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(useActionsPanelUI).AsSelf().AsImplementedInterfaces();


            // ViewModel
            builder.Register<CraftingViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<CookingActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<PutActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<TakeActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<FishingActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<RestingActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<DrinkingActionViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<CraftingModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register(
                _ => new PanelUIController(new List<IUIPanelController>
                    { buildingPanelUIController, questPanelUI, warehousePanelUIController }),
                Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
