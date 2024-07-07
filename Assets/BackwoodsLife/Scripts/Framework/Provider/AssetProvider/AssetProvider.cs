using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BackwoodsLife.Scripts.Framework.Provider.AssetProvider
{
    public sealed class AssetProvider : IAssetProvider
    {
        public async UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode)
        {
            await CheckAsset(assetId);
            return await Addressables.LoadSceneAsync(AssetConst.GameScene, loadSceneMode).Task;
        }

        public async UniTask<AsyncOperationHandle<GameObject>> LoadAssetAsync(string assetId)
        {
            await CheckAsset(assetId);
            return Addressables.LoadAssetAsync<GameObject>(assetId);
        }

        public async UniTask<AsyncOperationHandle<GameObject>> InstantiateAsync(string assetId, Transform parent = null)
        {
            await CheckAsset(assetId);
            return Addressables.InstantiateAsync(assetId, parent);
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId);
            return await handle.Task;
        }

        private async UniTask CheckAsset(string assetId)
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync(assetId, typeof(SceneInstance));
            await locationsHandle.Task;

            if (locationsHandle.Status != AsyncOperationStatus.Succeeded || locationsHandle.Result.Count <= 0)
                throw new NullReferenceException($"{assetId} not exist in addressables!");
        }

        private async UniTask CheckAsset(AssetReference assetId)
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync(assetId, typeof(SceneInstance));
            await locationsHandle.Task;

            if (locationsHandle.Status != AsyncOperationStatus.Succeeded || locationsHandle.Result.Count <= 0)
                throw new NullReferenceException($"{assetId} not exist in addressables!");
        }

        public void ServiceInitialization()
        {
        }

        public string Description => "AssetProvider";
    }
}
