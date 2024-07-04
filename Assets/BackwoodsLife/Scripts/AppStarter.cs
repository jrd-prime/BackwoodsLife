using BackwoodsLife.Scripts.Data.LoadingScreen;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Manager.DB;
using BackwoodsLife.Scripts.Framework.Manager.GameScene;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

// using Game.Scripts.Managers.Audio;
// using Game.Scripts.Managers.SaveLoad;
// using Game.Scripts.Providers.AssetProvider;

namespace BackwoodsLife.Scripts
{
    // TODO auth, cloud db
    public sealed class AppStarter : IInitializable
    {
        private IObjectResolver _container;

        private ILoader _loader;

        // private IAudioManager _audioManager;
        private IAssetProvider _assetProvider;

        // private ISaveAndLoadManager _saveAndLoadManager;
        private LoadingScreenViewModel _loadingScreenViewModel;
        private ILoadingOperation _dbManager;
        private GameSceneManager _gameSceneManager;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;
            Debug.LogWarning("inj " + container);
            _loader = _container.Resolve<ILoader>();
            _dbManager = _container.Resolve<IDBManager>();
            _loadingScreenViewModel = _container.Resolve<LoadingScreenViewModel>();
            _gameSceneManager = _container.Resolve<GameSceneManager>();
            // _audioManager = _container.Resolve<IAudioManager>();
            _assetProvider = _container.Resolve<IAssetProvider>();
            // _saveAndLoadManager = _container.Resolve<ISaveAndLoadManager>();
        }

        public async void Initialize()
        {
            // Начинаем асинхронную загрузку игровой сцены
            UniTask<SceneInstance> gameSceneTask =
                _assetProvider.LoadSceneAsync(AssetConst.GameScene, LoadSceneMode.Additive);

            /* Прокидываем таску загрузки сцены в сцен менеджер,
             * чтобы при инициализации сцен менеджера подождать полной загрузки сцены
             * и начать инициализацию игрока, окружения и т.д.
             * по сохраненным данным, если они есть,
             * или по дефолтным данным */
            _gameSceneManager.GameSceneLoadingTask = gameSceneTask;

            //  Add and initialize services
            // _loader.AddServiceToInitialize(_audioManager);
            // _loader.AddServiceToInitialize(_assetProvider);
            // _loader.AddServiceToInitialize(_saveAndLoadManager);
            _loader.AddServiceToInitialize(_dbManager);
            _loader.AddServiceToInitialize(_gameSceneManager);
            await _loader.StartServicesInitializationAsync(_loadingScreenViewModel);

            // TODO FadeOut loading screen view 
            // Unload current scene
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(_gameSceneManager.GameScene);
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
