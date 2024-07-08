using BackwoodsLife.Res.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Joystick
{
    public class JoystickView : UIView
    {
        public float moveSpeed = 5f;
        private Vector2 _moveInput;
        private Vector3 _moveDirection;

        private VisualElement _joystickHandle;
        private VisualElement _joystickRing;

        private readonly Vector3 _handleOffset = new(600, 190, 0);
        private readonly Vector3 _handleCenter = new(75, 75, 0);
        private Vector3 _offset;

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _joystickHandle = root.Q<VisualElement>("joystick-handle");
            _joystickRing = root.Q<VisualElement>("joystick-ring");

            root.RegisterCallback<PointerDownEvent>(OnPointerDown);
            root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            root.RegisterCallback<PointerUpEvent>(OnPointerUp);
            root.RegisterCallback<PointerOutEvent>(OnPointerCancel);

            _offset = _handleOffset + _handleCenter;
        }

        private void OnPointerCancel(PointerOutEvent evt) 
        {
            Debug.LogWarning("POINTER CANCEL");
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            UpdateJoystickPosition(evt.position);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            UpdateJoystickPosition(evt.position);
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            ResetJoystickPosition();
        }

        private void UpdateJoystickPosition(Vector3 position)
        {
            var po = position - _offset;

            Debug.LogWarning($"pos: {po}");
            _joystickHandle.transform.position = po;

            _moveInput = po / (_joystickRing.layout.width / 2);
            _moveInput = Vector2.ClampMagnitude(_moveInput, 1.0f);
        }

        private void ResetJoystickPosition()
        {
            _moveInput = Vector2.zero;
            _joystickHandle.transform.position = Vector2.zero;
        }

        private void Update()
        {
            _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y * -1f);
            transform.Translate(_moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
