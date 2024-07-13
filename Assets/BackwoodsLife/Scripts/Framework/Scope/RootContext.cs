using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers.JDebug;
using BackwoodsLife.Scripts.Framework.Helpers.JDebug.MemoryEtc;
using BackwoodsLife.Scripts.Framework.Manager.Audio;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.DB;
using BackwoodsLife.Scripts.Framework.Manager.Input;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Framework.Manager.SaveLoad;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using BackwoodsLife.Scripts.Framework.Scriptable;
using BackwoodsLife.Scripts.Framework.Scriptable.Configuration;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    // TODO Refactor in contexts
    public class RootContext : LifetimeScope
    {
        // [SerializeField] private ServicesConfig servicesConfig;
        [SerializeField] private DebugMemoryAndEtcView debugMemoryAndEtcView;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private EventSystem eventSystem;

        [FormerlySerializedAs("sMainConfiguration")] [SerializeField]
        private SMainConfigurations sMainConfigurations;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("RootContext");

            // Check(servicesConfig);
            Check(cameraController);
            // Check(gameSceneManager);
            Check(audioManager);

            // TODO services in config


            var input = Check(gameObject.AddComponent(typeof(MobileInput)));
            var audioProvider = Check(audioManager.GetComponentInChildren<AudioProvider>());
            var audioSourceProvider = Check(audioManager.GetComponentInChildren<AudioSourceProvider>());

            builder.RegisterInstance(sMainConfigurations);


            // builder.RegisterComponent(resourceManager).AsSelf();
            // builder.RegisterComponent(extractableResourcesConfigProvider).AsSelf();
            // Components
            builder.RegisterComponent(input).As<IInput>();
            builder.RegisterComponent(cameraController).As<ICameraController>();
            builder.RegisterComponent(audioManager).As<IAudioManager>();
            builder.RegisterComponent(audioProvider).As<IAudioProvider>();
            builder.RegisterComponent(audioSourceProvider).As<IAudioSourceProvider>();
            builder.RegisterComponent(eventSystem).AsSelf();


            // Services
            builder.Register<ConfigManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
            builder.Register(typeof(DBManager), Lifetime.Singleton).As<IDBManager>();
            builder.Register(typeof(DataBase), Lifetime.Singleton).As<IDataBase>();
            builder.Register(typeof(SaveAndLoadManager), Lifetime.Singleton).As<ISaveAndLoadManager>();
            builder.Register<InventoryManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Systems
            builder.Register<FollowSystem>(Lifetime.Singleton).AsSelf();

            builder.Register<InventoryModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<DebugMemoryAndEtcModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // ViewModel
            builder.Register<DebugMemoryAndEtcViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // View

            if (DebugConst.IsDebugOn) // TODO make it const
            {
                Check(debugMemoryAndEtcView);
                builder.RegisterComponent(debugMemoryAndEtcView);
            }
            else
                Destroy(debugMemoryAndEtcView.gameObject);
        }


        private T Check<T>(T component) where T : class
        {
            Assert.IsNotNull(component, $"{typeof(T)} is null. Add config to {this}");
            return component;
        }
    }
}
