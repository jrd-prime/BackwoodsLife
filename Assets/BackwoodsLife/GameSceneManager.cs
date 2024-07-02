using Cysharp.Threading.Tasks;
using Game.Scripts.Boostrap;
// using Game.Scripts.Managers.SaveLoad;
// using Game.Scripts.Player;
// using Game.Scripts.Providers.AssetProvider;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Scripts.Managers.GameScene
{
    public class GameSceneManager : MonoBehaviour, ILoadingOperation
    {
        public string Description => "Game Scene Manager";
        public UniTask<SceneInstance> GameSceneLoadingTask { private get; set; }
        public Scene GameScene { get; private set; }

        private SceneInstance _gameSceneInstance;
        private IObjectResolver _container;
        // private ISaveAndLoadManager _saveAndLoadManager;
        // private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;
            // _assetProvider = _container.Resolve<IAssetProvider>();
            // _saveAndLoadManager = _container.Resolve<ISaveAndLoadManager>();
        }

        public async void ServiceInitialization()
        {
            Debug.LogWarning("GAME SCENE LOADING TASK = " + GameSceneLoadingTask);

            _gameSceneInstance = await GameSceneLoadingTask;
            Assert.IsTrue(_gameSceneInstance.Scene.isLoaded, "Failed to load game scene");
            GameScene = _gameSceneInstance.Scene;
            await SetupPlayerAsync();
        }

        private async UniTask SetupPlayerAsync()
        {
            Debug.LogWarning("SETUP PLAYER");
            // var playerObj = FindObjectOfType<PlayerView>();
            // var playerData = _saveAndLoadManager.Get<PlayerModel>();

            // Debug.LogWarning($"player data = {playerData.Position} {playerData.Rotation}");
            await UniTask.CompletedTask;
        }
    }
}
