using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public Dictionary<AssetReferenceTexture2D, Sprite> IconCache2 { get; } = new();

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

        public async UniTask<GameObject> LoadAssetAsync(string assetId)
        {
            var handle = await Addressables.LoadAssetAsync<GameObject>(assetId);
            return handle;
        }

        public async UniTask<GameObject> LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject)
        {
            return await Addressables.LoadAssetAsync<GameObject>(assetReferenceGameObject);
        }

        public async UniTask<GameObject> InstantiateAsync(string assetId, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetId, parent);
            return await handle;
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId, Vector3 fixedPositionValue)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId);
            return await handle.Task;
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform position)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId, position);
            return await handle.Task;
        }

        //TODO remove
        public async UniTask<Sprite> LoadIconAsync(string elementTypeName)
        {
            if (IconCache.TryGetValue(elementTypeName, out Sprite iconFromCache)) return iconFromCache;

            var iconRef = _configManager.GetIconReference(elementTypeName);
            var icon = await Addressables.LoadAssetAsync<Sprite>(iconRef).Task;

            IconCache.TryAdd(elementTypeName, icon);

            return icon;
        }

        public Sprite GetIconFromRef(AssetReferenceTexture2D icon)
        {
            if (IconCache2.TryGetValue(icon, out Sprite iconFromCache)) return iconFromCache;

            Sprite iconNew = Addressables.LoadAssetAsync<Sprite>(icon).WaitForCompletion();

            IconCache2.TryAdd(icon, iconNew);

            return iconNew;
        }

        public Sprite GetIconFromName(string toString)
        {
            if (IconCache.TryGetValue(toString, out Sprite iconFromCache)) return iconFromCache;

            var iconNew = Addressables.LoadAssetAsync<Sprite>(toString).WaitForCompletion();

            IconCache.TryAdd(toString, iconNew);

            return iconNew;
        }
    }
}
