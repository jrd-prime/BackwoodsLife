using Game.Scripts.Boostrap;
using Game.Scripts.Managers.GameScene;
using Game.Scripts.UI.Boostrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Scope
{
    public class BoostrapContext : LifetimeScope
    {
        [SerializeField] private LoadingScreenView loadingScreenView;
        [SerializeField] private GameSceneManager gameSceneManager;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("BoostrapContext");
            // Services
            builder.Register<LoadingScreenProvider>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponent(gameSceneManager);

            // Models
            builder.Register<Loader>(Lifetime.Singleton).AsImplementedInterfaces();

            // ViewModels
            builder.Register<LoadingScreenViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // View
            builder.RegisterComponent(loadingScreenView);

            // Entry points
            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
