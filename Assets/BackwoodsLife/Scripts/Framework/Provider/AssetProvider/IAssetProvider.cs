using BackwoodsLife.Scripts.Framework.Bootstrap;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BackwoodsLife.Scripts.Framework.Provider.AssetProvider
{
    public interface IAssetProvider : ILoadingOperation
    {
        UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        UniTask<AsyncOperationHandle<GameObject>> LoadAssetAsync(string assetId);
        UniTask<AsyncOperationHandle<GameObject>> InstantiateAsync(string assetId, Transform parent = null);
    }
}
