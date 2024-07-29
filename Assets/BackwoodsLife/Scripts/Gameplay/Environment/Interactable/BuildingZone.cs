using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.System;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable
{
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private SWorldItemConfigNew worldItemConfig;

        private InteractSystem _interactSystem;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player) return;
            Debug.Log($"Char in trigger zone! {name} / {worldItemConfig.InteractTypes}");
            _interactSystem.Build(ref worldItemConfig);
        }
    }
}
