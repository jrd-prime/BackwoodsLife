using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BackwoodsLife.Scripts.Framework.Provider.AssetProvider
{
    public interface IAssetProvider : ILoadingOperation
    {
        public Dictionary<string, Sprite> IconCache { get; }
        public UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        public UniTask<GameObject> LoadAssetAsync(string assetId);
        public UniTask<GameObject> LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject);
        public UniTask<GameObject> InstantiateAsync(string assetId, Transform parent = null);
        public UniTask<GameObject> InstantiateAsync(AssetReference assetId, Vector3 fixedPositionValue);
        public UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform position = null);
        public UniTask<Sprite> LoadIconAsync(string elementTypeName);
        public Sprite GetIconFromRef(AssetReferenceTexture2D icon);
    }
}
