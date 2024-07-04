using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    public class JoystickController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Vector2 moveInput;
        private Vector3 moveDirection;

        private VisualElement joystickHandle;
        private VisualElement joystickRing;

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            joystickHandle = root.Q<VisualElement>("joystick-handle");
            joystickRing = root.Q<VisualElement>("joystick-ring");

            root.RegisterCallback<PointerDownEvent>(OnPointerDown);
            root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            root.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            UpdateJoystickPosition(evt.position);
            joystickHandle.transform.position = evt.localPosition - new Vector3(75f, 75f, 0f);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            UpdateJoystickPosition(evt.position);
            joystickHandle.transform.position = evt.localPosition - new Vector3(75f, 75f, 0f);
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            ResetJoystickPosition();
        }

        private void UpdateJoystickPosition(Vector2 position)
        {
            Vector2 joystickCenter = joystickRing.layout.center;
            moveInput = (position - joystickCenter) / (joystickRing.layout.width / 2);
            moveInput = Vector2.ClampMagnitude(moveInput, 1.0f);

            Debug.LogWarning(moveInput);
            // joystickHandle.transform.position = position;
        }

        private void ResetJoystickPosition()
        {
            moveInput = Vector2.zero;
            joystickHandle.transform.position = Vector2.zero;
        }

        private void Update()
        {
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y * -1f);
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
