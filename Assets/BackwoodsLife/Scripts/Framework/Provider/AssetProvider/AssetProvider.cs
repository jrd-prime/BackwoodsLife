using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Provider.AssetProvider
{
    public sealed class AssetProvider : IAssetProvider
    {
        public string Description => "AssetProvider";
        public Dictionary<string, Sprite> IconCache { get; } = new();

        private IConfigManager _configManager;

        [Inject]
        private void Construct(IConfigManager configManager) => _configManager = configManager;

        public void ServiceInitialization()
        {
            Addressables.InitializeAsync();
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode)
        {
            return await Addressables.LoadSceneAsync(AssetConst.GameScene, loadSceneMode).Task;
        }

        public async UniTask<AsyncOperationHandle<GameObject>> LoadAssetAsync(string assetId)
        {
            return Addressables.LoadAssetAsync<GameObject>(assetId);
        }

        public async UniTask<AsyncOperationHandle<GameObject>> InstantiateAsync(string assetId, Transform parent = null)
        {
            return Addressables.InstantiateAsync(assetId, parent);
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId);
            return await handle.Task;
        }

        public async UniTask<Sprite> LoadIconAsync(string elementTypeName)
        {
            if (IconCache.TryGetValue(elementTypeName, out Sprite iconFromCache)) return iconFromCache;

            var iconRef = _configManager.GetIconReference(elementTypeName);
            if (iconRef == null) throw new NullReferenceException($"Icon not found for {elementTypeName}");

            var icon = await Addressables.LoadAssetAsync<Sprite>(iconRef).Task;

            IconCache.TryAdd(elementTypeName, icon);

            return icon;
        }
    }
}
