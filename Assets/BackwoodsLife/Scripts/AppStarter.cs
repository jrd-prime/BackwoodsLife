using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Manager.Audio;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.DB;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.GameScene;
using BackwoodsLife.Scripts.Framework.Manager.SaveLoad;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using BackwoodsLife.Scripts.Framework.Provider.Recipe;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts
{
    // TODO auth, cloud db
    public sealed class AppStarter : IInitializable
    {
        private ILoader _loader;
        private IConfigManager _configManager;
        private IAudioManager _audioManager;
        private IAssetProvider _assetProvider;
        private ISaveAndLoadManager _saveAndLoadManager;
        private ILoadingOperation _dbManager;
        private GameSceneManager _gameSceneManager;
        private GameDataManager _gameDataManager;
        private IRecipeProvider _recipeProvider;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _loader = container.Resolve<ILoader>();
            _configManager = container.Resolve<IConfigManager>();
            _recipeProvider = container.Resolve<IRecipeProvider>();
            _dbManager = container.Resolve<IDBManager>();
            _gameSceneManager = container.Resolve<GameSceneManager>();
            _audioManager = container.Resolve<IAudioManager>();
            _assetProvider = container.Resolve<IAssetProvider>();
            _saveAndLoadManager = container.Resolve<ISaveAndLoadManager>();
            _gameDataManager = container.Resolve<GameDataManager>();
        }

        public async void Initialize()
        {
            // Starting asynchronous loading of the game scene
            UniTask<SceneInstance> gameSceneTask =
                _assetProvider.LoadSceneAsync(AssetConst.GameScene, LoadSceneMode.Additive);

            /* Passing the scene loading task to the scene manager,
             * so that during the scene manager initialization,
             * we wait for the scene to be fully loaded
             * before starting the initialization of the player, environment, etc.
             * using the saved data, if available,
             * or using the default data */
            _gameSceneManager.GameSceneLoadingTask = gameSceneTask;

            // Adding and initializing services
            _loader.AddServiceToInitialize(_configManager);
            _loader.AddServiceToInitialize(_recipeProvider);
            _loader.AddServiceToInitialize(_audioManager);
            _loader.AddServiceToInitialize(_assetProvider);
            _loader.AddServiceToInitialize(_dbManager);
            _loader.AddServiceToInitialize(_saveAndLoadManager);
            _loader.AddServiceToInitialize(_gameSceneManager);
            _loader.AddServiceToInitialize(_gameDataManager);

            await _loader.StartServicesInitializationAsync();


            // TODO FadeOut loading screen view 
            // Unloading current scene
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(_gameSceneManager.GameScene);
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
