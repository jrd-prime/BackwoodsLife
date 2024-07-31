﻿using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Interact.System;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.Warehouse;
using BackwoodsLife.Scripts.Gameplay.Player;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using BackwoodsLife.Scripts.Gameplay.UI.InteractPanel;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class GameSceneContext : LifetimeScope
    {
        [SerializeField] private UIFrameController uiFrameController;
        [SerializeField] private InteractSystem interactSystem;
        [SerializeField] private CharacterOverUI characterOverUIHolder;
        [SerializeField] private InteractPanelUI interactPanelUIHolder;


        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("GameSceneContext");
            // builder.Register<NavMeshManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            // builder.Register<PlayerStateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PlayerViewModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<JoystickViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WarehouseViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<CollectSystem>(Lifetime.Singleton).AsSelf();

            // Systems
            // builder.Register<GroundSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<GatherableSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<JoystickModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.RegisterEntryPoint<PlayerLoop>();
            //
            // builder.RegisterEntryPoint<PlayerViewModel>();

            builder.RegisterComponent(uiFrameController).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactSystem).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(characterOverUIHolder).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(interactPanelUIHolder).AsSelf().AsImplementedInterfaces();
        }
    }
}
