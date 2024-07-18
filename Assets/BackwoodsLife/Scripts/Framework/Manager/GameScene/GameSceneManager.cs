using BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Interactable.Config;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

// using Game.Scripts.Managers.SaveLoad;
// using Game.Scripts.Player;
// using Game.Scripts.Providers.AssetProvider;

namespace BackwoodsLife.Scripts.Framework.Manager.GameScene
{
    public class GameSceneManager : MonoBehaviour, ILoadingOperation
    {
        public string Description => "Game Scene Manager";
        public UniTask<SceneInstance> GameSceneLoadingTask { private get; set; }
        public Scene GameScene { get; private set; }

        private SceneInstance _gameSceneInstance;

        private IObjectResolver _container;

        private IConfigManager _configManager;

        // private ISaveAndLoadManager _saveAndLoadManager;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;
            _configManager = container.Resolve<IConfigManager>();
            _assetProvider = _container.Resolve<IAssetProvider>();
            // _saveAndLoadManager = _container.Resolve<ISaveAndLoadManager>();
        }

        public async void ServiceInitialization()
        {
            Debug.Log("GAME SCENE LOADING TASK = " + GameSceneLoadingTask);

            _gameSceneInstance = await GameSceneLoadingTask;
            Assert.IsTrue(_gameSceneInstance.Scene.isLoaded, "Failed to load game scene");
            GameScene = _gameSceneInstance.Scene;
            await SetupPlayerAsync();
            await SetupStaticObjectsAsync();
        }

        private async UniTask SetupPlayerAsync()
        {
            Debug.Log("SETUP PLAYER");
            // var playerObj = FindObjectOfType<PlayerView>();
            // var playerData = _saveAndLoadManager.Get<PlayerModel>();

            // Debug.LogWarning($"player data = {playerData.Position} {playerData.Rotation}");
            await UniTask.CompletedTask;
        }

        private async UniTask SetupStaticObjectsAsync()
        {
            Debug.Log("SetupStaticObjectsAsync");
            // var a = _configManager.GetConfig<SStaticInteractableObjectsList>();
            // Debug.Log(a);

            // foreach (var av in a.staticInteractables)
            // {
            //     Debug.Log($"av = {av}");
            //
            //     var position = av.fixedPosition;
            //     if (position == Vector3.zero)
            //     {
            //         Debug.Log("POSITION NOT SET for " + av.name);
            //     }
            //
            //     AssetReferenceGameObject prefab;
            //
            //     if (av.hasLevels)
            //     {
            //         // get prefab lvl from save
            //         prefab = av.levelsData[0].assetReference;
            //     }
            //     else
            //     {
            //         prefab = av.defaultLevelData.assetReference;
            //     }
            //
            //     var obj = await _assetProvider.InstantiateAsync(prefab);
            //
            //
            //     var go = obj;
            //     Debug.Log($"go = {go} {go.transform.position}");
            //     go.transform.position = position;
            // }


            // var playerObj = FindObjectOfType<PlayerView>();
            // var playerData = _saveAndLoadManager.Get<PlayerModel>();

            // Debug.LogWarning($"player data = {playerData.Position} {playerData.Rotation}");
            await UniTask.CompletedTask;
        }
    }
}
