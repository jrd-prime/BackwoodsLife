using System;
using BackwoodsLife.Scripts.Framework.Extensions.Helpers;
using BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact
{
    [RequireComponent(typeof(CapsuleCollider))]
    public sealed class InteractZone : MonoBehaviour
    {
        public string InteractObjName { get; private set; }
        private Action<IInteractZoneState> _interactZoneStateCallback;
        private IInteractZoneState _currentState;

        private void Awake()
        {
            _currentState = null;
            _interactZoneStateCallback += InteractZoneStateCallback;
        }

        private void InteractZoneStateCallback(IInteractZoneState state)
        {
            // TODO states
            Debug.LogWarning($"Zone callback. {state.StateDesc}");

            ChangeState(state);
        }

        [Inject]
        private void Construct()
        {
            Debug.LogWarning("Trigger zone init");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || other.gameObject.layer != (int)JLayers.Player) return;

            Transform parentTransform = transform.parent;

            if (parentTransform != null)
            {
                var interactable = parentTransform.GetComponent<InteractableItemBase>();
                Assert.IsNotNull(interactable, $"Interactable is null on {parentTransform.name} prefab");


                Debug.LogWarning(
                    $"<color=yellow>Interact zone:\t{interactable.GetType().Name}\t{interactable.interactType}</color>");

                interactable.Process(_interactZoneStateCallback);
            }
            else
                Debug.LogWarning(
                    $"{transform.name} Текущий объект не имеет родителя. Возможно он не создан в редакторе. ");
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.LogWarning("on trigger exit");
            if (_currentState == null) return;

            _currentState.Exit();
            _currentState = null;
        }

        public void DestroyO()
        {
            transform.parent.gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }

        private void ChangeState(IInteractZoneState state)
        {
            if (_currentState == state) return;
            _currentState?.Exit();
            _currentState = state;
            state.Enter(this);
        }
    }
}
