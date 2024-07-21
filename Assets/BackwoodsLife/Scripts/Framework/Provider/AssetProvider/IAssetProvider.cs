using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BackwoodsLife.Scripts.Framework.Provider.AssetProvider
{
    public interface IAssetProvider : ILoadingOperation
    {
        public Dictionary<string, Sprite> IconCache { get; }
        public UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        public UniTask<AsyncOperationHandle<GameObject>> LoadAssetAsync(string assetId);
        public UniTask<AsyncOperationHandle<GameObject>> InstantiateAsync(string assetId, Transform parent = null);

        public UniTask<GameObject> InstantiateAsync(AssetReference assetId);
        public UniTask<Sprite> LoadIconAsync(string elementTypeName);
    }
}
