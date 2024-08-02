using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework
{
    public class BuildSystem
    {
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void BuildAsync(SWorldItemConfigNew worldItemConfig, Action onBuildFinish)
        {
            Debug.LogWarning("Build");

            var levelConfig = worldItemConfig.upgradeConfig.GetLevel(ELevel.Level_1);

            var prefabRef = levelConfig.levelPrefabReference;
            var prefab = await _assetProvider.InstantiateAsync(prefabRef);

            if (worldItemConfig.fixedPosition) prefab.transform.position = worldItemConfig.fixedPositionValue;

            onBuildFinish.Invoke();
        }
    }
}
