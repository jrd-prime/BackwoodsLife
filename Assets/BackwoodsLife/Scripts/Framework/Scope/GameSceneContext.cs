// using Game.Scripts.Managers.NavMesh;
// using Game.Scripts.Player;
// using Game.Scripts.Player.Interface;
// using Game.Scripts.Player.StateMachine;
// using Game.Scripts.Systems;

using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class GameSceneContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("GameSceneContext Config");

            // builder.Register<NavMeshManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            //
            //
            // builder.Register<PlayerStateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<PlayerViewModel>(Lifetime.Singleton).As<IPlayerViewModel>();
            //
            // // Systems
            // builder.Register<GroundSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<GatherableSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            //
            // // Model
            // builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.RegisterEntryPoint<PlayerLoop>();
        }
    }
}
