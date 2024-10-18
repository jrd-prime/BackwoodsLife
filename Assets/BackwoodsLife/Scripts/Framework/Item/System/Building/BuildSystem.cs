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

        public async void BuildAsync(WorldItemSettings worldItemSettings, LevelType levelType,
            Action<string, LevelType> onBuildComplete)
        {
            Debug.LogWarning("Build");

            var levelConfig = worldItemSettings.upgradeConfig.GetLevel(levelType);

            var prefabRef = levelConfig.levelPrefabReference;
            var prefab = await _assetProvider.InstantiateAsync(prefabRef);

            if (worldItemSettings.fixedPosition) prefab.transform.position = worldItemSettings.fixedPositionValue;

            // add building to buildings cache
            onBuildComplete.Invoke(worldItemSettings.itemName, levelType);
        }
    }
}
