using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private SWorldItemConfigNew worldItemConfig;
        private IAssetProvider _assetProvider;

        private const int BuildingStartLevel = 0;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        private void Awake()
        {
            if (_assetProvider == null)
                throw new NullReferenceException(
                    $"AssetProvider not injected! Add {name} prefab to BuildingZonesContext prefab into auto inject");
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player) return;

            Debug.LogWarning($"Char in trigger zone! {name}");

            if (worldItemConfig == null)
                throw new NullReferenceException(
                    $"{worldItemConfig.name} upgradeConfig is null! Check {worldItemConfig.name} config!");

            switch (worldItemConfig.InteractTypes)
            {
                case EInteractTypes.Collect:
                    break;
                case EInteractTypes.Use:
                    break;
                case EInteractTypes.Upgrade:
                    break;
                case EInteractTypes.UseAndUpgrade:
                    Debug.LogWarning("Use And Upgrade");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //
            // var upgradeConfig = worldItemConfig.upgradeConfig;
            //
            //
            // var prefab = upgradeConfig.GetLevel(BuildingStartLevel);
            //
            // if (prefab == null)
            //     throw new NullReferenceException(
            //         $"{worldItemConfig.name} prefab in upgradeConfig is null for level {BuildingStartLevel + 1}! Check {worldItemConfig.name} config!");
            //
            // Debug.LogWarning(worldItemConfig.fixedPositionValue);
            //
            // var obj = await _assetProvider.InstantiateAsync(prefab);
            //
            // obj.transform.position = worldItemConfig.fixedPositionValue;
            // DestroyImmediate(gameObject);
        }
    }
}
