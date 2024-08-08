using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private SWorldItemConfig worldItemConfig;

        public Action<Dictionary<SItemConfig, int>> OnBuildStarted;
        public Action OnBuildFinished;

        private InteractSystem _interactSystem;
        private bool _isInTriggerZone;
        private BuildSystem _buildSystem;


        [Inject]
        private void Construct(InteractSystem interactSystem, BuildSystem buildSystem)
        {
            _interactSystem = interactSystem;
            _buildSystem = buildSystem;
        }

        private void Awake()
        {
            if (worldItemConfig == null)
                throw new NullReferenceException(
                    $"{worldItemConfig.name} upgradeConfig is null! Check {worldItemConfig.name} config!");
            if (_interactSystem == null)
                throw new NullReferenceException("InteractSystem does not inject!");
            if (_buildSystem == null)
                throw new NullReferenceException("BuildSystem does not inject!");

            OnBuildStarted += OnBuildStart;
            OnBuildFinished += OnBuildFinish;
        }


        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player) return;

            _isInTriggerZone = true;

            while (_isInTriggerZone && _interactSystem.IsMoving)
            {
                // Debug.LogWarning("In zone but still moving, waiting 100ms");
                await UniTask.Delay(100);
            }

            if (!_isInTriggerZone || _interactSystem.IsMoving) return;

            Debug.LogWarning("In zone and not moving, building!");
            Debug.Log($"Char in trigger zone! {name} / {worldItemConfig.interactTypes}");
            _interactSystem.OnBuildZoneEnter(in worldItemConfig, OnBuildStarted);
        }

        private void OnBuildStart(Dictionary<SItemConfig, int> levelResources)
        {
            Debug.LogWarning("On build start");

            _interactSystem.SpendResourcesForBuild(levelResources);
            _buildSystem.BuildAsync(worldItemConfig, OnBuildFinish);
        }

        private void OnBuildFinish()
        {
            Debug.LogWarning("On build finish");
            Destroy(gameObject);
            OnLeaveZone();
        }

        private void OnLeaveZone()
        {
            _isInTriggerZone = false;
            Debug.LogWarning($"Char leave zone! {name} / {worldItemConfig.interactTypes}");
            _interactSystem.OnBuildZoneExit();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInTriggerZone) return;
            Debug.LogWarning($"Char exit from trigger zone! {name} / {worldItemConfig.interactTypes}");
            OnLeaveZone();
        }
    }
}
