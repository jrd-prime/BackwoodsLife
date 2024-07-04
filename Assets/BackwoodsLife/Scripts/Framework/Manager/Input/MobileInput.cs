using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    public struct TouchData
    {
        public int LayerId;
        public Vector2 TouchPositionOnScreen;
    }

    public sealed class MobileInput : MonoBehaviour, IInput
    {
        // public event Action onLMousePress;
        // public event Action<Vector2> onLMouseDrag;
        // public event Action onLMouseRelease;
        // public event Action onLMouseTaped;
        // public event Action<Vector3> OnSingleClick;

        public ReactiveProperty<Vector2> TouchV2 { get; } = new();

        public ReactiveProperty<TouchData> TouchWithData { get; } = new ReactiveProperty<TouchData>();

        private JInputActions _gameInputActions;
        private EventSystem _event;

        public void ChangeActionMap(string actionMapName)
        {
            throw new NotImplementedException();
        }

        [Inject]
        private void Construct(EventSystem eventSystem)
        {
            Debug.LogWarning("CONSTRUCT INPUT");
            _event = eventSystem;
        }

        private void Awake()
        {
            _gameInputActions = new JInputActions();
            _gameInputActions.Enable();

            _gameInputActions.UI.TouchPosition.performed += OnTouchData;
            // _gameInputActions.UI.Click.performed += OnTouchData;
        }

        private void OnTouchch(InputAction.CallbackContext obj)
        {
            Debug.LogWarning("OJO NWSBSDIFS ODJF PSDFKS DKF ++++==== " + obj);
        }

        private bool IsPointerOverUI(Vector2 position)
        {
            var pointer = new PointerEventData(EventSystem.current);
            pointer.position = position;
            var raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointer, raycastResults);

            return raycastResults.Count > 0 && raycastResults.Any(result => result is { distance: 0, isValid: true });
        }

        private void OnTouchData(InputAction.CallbackContext obj)
        {
            var position = obj.ReadValue<Vector2>();

            if (IsPointerOverUI(position)) return;


            TouchV2.Value = position;

            // TODO inject camera
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Debug.LogWarning("COLLAL LA LLA LA LA L = " + hit.collider);


                // UnityEngine.Debug.Log("Touched object: " + hit.collider.name);
                // UnityEngine.Debug.LogWarning("hti point " + hit.point);
                // UnityEngine.Debug.LogWarning("collider layer = " + hit.collider.gameObject.layer);


                var layer = hit.collider.gameObject.layer;

                // Debug.LogWarning(" L AY E R !!!! ====   " + layer);


                if (position == Vector2.zero) return;

                TouchWithData.Value = new TouchData() { LayerId = layer, TouchPositionOnScreen = position };

                // Implement your logic for interacting with the touched object
                HandleTouch(hit);
            }
        }

        private void OnTouch(InputAction.CallbackContext obj)
        {
//             var o = obj.ReadValue<Vector2>();
//
//             // Debug.LogWarning("??????? 2222      ?????" + o);
//             TouchV2.Value = o;
// //TODO inject camera
//             Ray ray = UnityEngine.Camera.main.ScreenPointToRay(o);
//             RaycastHit hit;
//
//             // Perform the raycast
//             if (Physics.Raycast(ray, out hit))
//             {
//                 // Debug.Log("Touched object: " + hit.collider.name);
//                 // Debug.LogWarning("hti point " + hit.point);
//                 // Debug.LogWarning("collider layer = " + hit.collider.gameObject.layer);
//
//                 // Debug.LogWarning(JLayers.Ground);
//                 var layer = hit.collider.gameObject.layer;
//
//                 if (layer == (int)JLayers.Ground)
//                 {
//                     // move player???
//                 }
//
//                 // Implement your logic for interacting with the touched object
//                 HandleTouch(hit);
//             }
        }

        private void HandleTouch(RaycastHit hit)
        {
            // Example logic: Change color of the touched object
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            // if (renderer != null)
            // {
            //     renderer.material.color = Color.gray;
            // }
        }

        private void OnSinClick(InputAction.CallbackContext obj)
        {
            // Debug.LogWarning("On single click" + obj.ReadValueAsObject());
        }
    }
}
