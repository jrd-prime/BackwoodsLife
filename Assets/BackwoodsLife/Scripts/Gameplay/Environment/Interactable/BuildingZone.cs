using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private SWorldItemConfigNew worldItemConfig;

        private InteractSystem _interactSystem;
        private bool _isInTriggerZone;

        [Inject]
        private void Construct(InteractSystem interactSystem) => _interactSystem = interactSystem;

        private void Awake()
        {
            if (worldItemConfig == null)
                throw new NullReferenceException(
                    $"{worldItemConfig.name} upgradeConfig is null! Check {worldItemConfig.name} config!");
            if (_interactSystem == null)
                throw new NullReferenceException("InteractSystem does not inject!");
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player) return;

            _isInTriggerZone = true;

            while (_isInTriggerZone && _interactSystem.IsMoving)
            {
                Debug.LogWarning("In zone but still moving, waiting 100ms");
                await UniTask.Delay(100);
            }

            if (!_isInTriggerZone || _interactSystem.IsMoving) return;

            Debug.LogWarning("In zone and not moving, building!");
            Debug.Log($"Char in trigger zone! {name} / {worldItemConfig.InteractTypes}");
            _interactSystem.OnBuildZoneEnter(ref worldItemConfig);
        }

        private void OnTriggerExit(Collider other)
        {
            _isInTriggerZone = false;
            if (other.gameObject.layer != (int)JLayers.Player) return;
            Debug.LogWarning($"Char exit from trigger zone! {name} / {worldItemConfig.InteractTypes}");

            _interactSystem.OnBuildZoneExit();
        }
    }
}
