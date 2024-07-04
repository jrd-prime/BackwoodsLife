using BackwoodsLife.Scripts.Data.LoadingScreen;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Manager.GameScene;
using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;
using BackwoodsLife.Scripts.Gameplay.UI.LoadingScreen;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
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

            // Entry points
            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
