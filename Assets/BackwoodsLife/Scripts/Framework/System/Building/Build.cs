using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.System.Building
{
    public sealed class Build : IBuildingSystem
    {
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void BuildAsync(SWorldItemConfig worldItemConfig)
        {
            Debug.LogWarning("Build");

            var levelConfig = worldItemConfig.upgradeConfig.GetLevel(ELevel.Level_1);

            var prefabRef = levelConfig.levelPrefabReference;
            var prefab = await _assetProvider.InstantiateAsync(prefabRef);

            if (worldItemConfig.fixedPosition) prefab.transform.position = worldItemConfig.fixedPositionValue;

            // add building to buildings cache
        }
    }
}
