using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.System.Building;
using BackwoodsLife.Scripts.Framework.System.WorldItem;
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

        private Interact _interact;
        private bool _isInTriggerZone;
        private Build _build;


        [Inject]
        private void Construct(Interact interact, Build build)
        {
            _interact = interact;
            _build = build;
        }

        private void Awake()
        {
            if (worldItemConfig == null)
                throw new NullReferenceException(
                    $"{worldItemConfig.name} upgradeConfig is null! Check {worldItemConfig.name} config!");
            if (_interact == null)
                throw new NullReferenceException("InteractSystem does not inject!");
            if (_build == null)
                throw new NullReferenceException("BuildSystem does not inject!");

            OnBuildStarted += OnBuildStart;
            OnBuildFinished += OnBuildFinish;
        }


        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player) return;

            _isInTriggerZone = true;

            while (_isInTriggerZone && _interact.IsMoving)
            {
                // Debug.LogWarning("In zone but still moving, waiting 100ms");
                await UniTask.Delay(100);
            }

            if (!_isInTriggerZone || _interact.IsMoving) return;

            Debug.LogWarning("In zone and not moving, building!");
            Debug.Log($"Char in trigger zone! {name} / {worldItemConfig.interactTypes}");
            _interact.OnBuildZoneEnter(in worldItemConfig, OnBuildStarted);
        }

        private void OnBuildStart(Dictionary<SItemConfig, int> levelResources)
        {
            Debug.LogWarning("On build start");

            _interact.SpendResourcesForBuild(levelResources);
            _build.BuildAsync(worldItemConfig, OnBuildFinish);
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
            _interact.OnBuildZoneExit();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInTriggerZone) return;
            Debug.LogWarning($"Char exit from trigger zone! {name} / {worldItemConfig.interactTypes}");
            OnLeaveZone();
        }
    }
}
