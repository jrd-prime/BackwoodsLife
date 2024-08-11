﻿using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using BackwoodsLife.Scripts.Framework.Item.System;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Framework.Item.System.Item;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Quest;
using BackwoodsLife.Scripts.Gameplay.Environment;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.InteractPanel;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActionsPanel;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.Warehouse;
using BackwoodsLife.Scripts.Gameplay.UI.UIButtons;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
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
        [SerializeField] private QuestPanelUIController questPanelUIController;
        [SerializeField] private WarehousePanelUIController warehousePanelUIController;
        [SerializeField] private InteractItemInfoPanelUI interactItemInfoPanelUIController;
        [SerializeField] private UseActionsPanelUIController useActionsPanelUIController;


        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("GameSceneContext");

            Assert.IsNotNull(buildingPanelUIController,
                "buildingPanelController is null. Add prefab to scene and set prefab to " + name);
            Assert.IsNotNull(questPanelUIController,
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
            builder.RegisterComponent(useActionsPanelUIController).AsSelf().AsImplementedInterfaces();


            builder.Register(
                _ => new PanelUIController(new List<IUIPanelController>
                    { buildingPanelUIController, questPanelUIController, warehousePanelUIController }),
                Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
