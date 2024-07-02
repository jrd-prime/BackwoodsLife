// using Game.Scripts.Helpers.JDebug;
// using Game.Scripts.Helpers.JDebug.MemoryEtc;
// using Game.Scripts.Managers.Audio;
// using Game.Scripts.Managers.Camera;
// using Game.Scripts.Managers.DataBase;
// using Game.Scripts.Managers.Input;
// using Game.Scripts.Managers.Pools;
// using Game.Scripts.Managers.SaveLoad;
// using Game.Scripts.Prefabs.Extractable;
// using Game.Scripts.Providers.AssetProvider;
// using Game.Scripts.Scriptable;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Scope
{
    // TODO Refactor in contexts
    public class RootContext : LifetimeScope
    {
        // [SerializeField] private ServicesConfig servicesConfig;
        // [SerializeField] private DebugMemoryAndEtcView debugMemoryAndEtcView;
        // [SerializeField] private CameraController cameraController;
        // [SerializeField] private AudioManager audioManager;
        // [SerializeField] private EventSystem eventSystem;
        // [SerializeField] private ResourceManager resourceManager;
        // [SerializeField] private ExtractableResourcesConfigProvider extractableResourcesConfigProvider;


        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("RootContext");

            // Check(servicesConfig);
            // Check(cameraController);
            // // Check(gameSceneManager);
            // Check(audioManager);

            // TODO services in config


            // var input = Check(gameObject.AddComponent(typeof(MobileInput)));
            // var audioProvider = Check(audioManager.GetComponentInChildren<AudioProvider>());
            // var audioSourceProvider = Check(audioManager.GetComponentInChildren<AudioSourceProvider>());
            //
            //
            // builder.RegisterComponent(resourceManager).AsSelf();
            // builder.RegisterComponent(extractableResourcesConfigProvider).AsSelf();
            // // Components
            // builder.RegisterComponent(input).As<IInput>();
            // builder.RegisterComponent(cameraController).As<ICameraController>();
            // builder.RegisterComponent(audioManager).As<IAudioManager>();
            // builder.RegisterComponent(audioProvider).As<IAudioProvider>();
            // builder.RegisterComponent(audioSourceProvider).As<IAudioSourceProvider>();
            //
            // builder.RegisterComponent(eventSystem).AsSelf();


            // Services
            // builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
            // builder.Register(typeof(DBManager), Lifetime.Singleton).As<IDBManager>();
            // builder.Register(typeof(DataBase), Lifetime.Singleton).As<IDataBase>();
            // builder.Register(typeof(SaveAndLoadManager), Lifetime.Singleton).As<ISaveAndLoadManager>();
            // builder.Register<FollowSystem>(Lifetime.Singleton).AsSelf();
            //
            //
            // builder.Register<DebugMemoryAndEtcModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // // ViewModel
            // builder.Register<DebugMemoryAndEtcViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // // View
            //
            // if (DebugConst.IsDebugOn) // TODO make it const
            // {
            //     Check(debugMemoryAndEtcView);
            //     builder.RegisterComponent(debugMemoryAndEtcView);
            // }
            // else
            //     Destroy(debugMemoryAndEtcView.gameObject);
        }


        private T Check<T>(T component) where T : class
        {
            Assert.IsNotNull(component, $"{typeof(T)} is null. Add config to {this}");
            return component;
        }
    }
}
