﻿using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Quest;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.InteractPanel;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
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
        [SerializeField] private InteractSystem interactSystem;
        [SerializeField] private CharacterOverUI characterOverUIHolder;
        [SerializeField] private InteractPanelUI interactPanelUIHolder;
        [SerializeField] private BuildingPanelUIController buildingPanelUIController;
        [SerializeField] private QuestPanelUIController questPanelUIController;
        [SerializeField] private WarehousePanelUIController warehousePanelUIController;


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

            builder.Register<CollectSystem>(Lifetime.Singleton).AsSelf();

            builder.Register<WarehouseManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<QuestManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<JoystickModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<BuildingPanelFiller>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterComponent(buildingPanelUIController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(uiFrameController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(uiButtonsController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactSystem).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(characterOverUIHolder).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactPanelUIHolder).AsSelf().AsImplementedInterfaces();


            builder.Register(
                _ => new PanelUIController(new List<IUIPanelController>
                    { buildingPanelUIController, questPanelUIController, warehousePanelUIController }),
                Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
