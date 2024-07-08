using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Gameplay.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class GameSceneContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("GameSceneContext");

            // builder.Register<NavMeshManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            // builder.Register<PlayerStateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PlayerViewModel>(Lifetime.Singleton).As<IPlayerViewModel>();

            // Systems
            // builder.Register<GroundSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<GatherableSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.RegisterEntryPoint<PlayerLoop>();
        }
    }
}
