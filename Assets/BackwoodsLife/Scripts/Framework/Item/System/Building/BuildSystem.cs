using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.System.Building
{
    public sealed class BuildSystem : IBuildingSystem
    {
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void BuildAsync(SWorldItemConfig worldItemConfig, ELevel level,
            Action<string, ELevel> onBuildComplete)
        {
            Debug.LogWarning("Build");

            var levelConfig = worldItemConfig.upgradeConfig.GetLevel(level);

            var prefabRef = levelConfig.levelPrefabReference;
            var prefab = await _assetProvider.InstantiateAsync(prefabRef);

            if (worldItemConfig.fixedPosition) prefab.transform.position = worldItemConfig.fixedPositionValue;

            // add building to buildings cache
            onBuildComplete.Invoke(worldItemConfig.itemName, level);
        }
    }
}
