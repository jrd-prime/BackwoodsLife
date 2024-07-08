using System;
using BackwoodsLife.Scripts.Data;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI.Joystick
{
    public class JoystickModel : IDataModel
    {
        public readonly ReactiveProperty<Vector3> MoveDirection = new(Vector3.zero);
        public readonly ReactiveProperty<Vector3> ScreenOffset = new(Vector3.zero);
        public VisualElement JoystickHandle { get; private set; }
        public VisualElement JoystickRing { get; private set; }


        public void SetJoystickHandle(VisualElement joystickHandle)
        {
            CheckExpectedElement(joystickHandle, UIName.JoystickHandle);
            JoystickHandle = joystickHandle;
        }

        public void SetJoystickRing(VisualElement joystickRing)
        {
            CheckExpectedElement(joystickRing, UIName.JoystickRing);
            JoystickRing = joystickRing;
        }

        private void CheckExpectedElement(VisualElement expectedElement, string expectedElementName)
        {
            Assert.IsNotNull(expectedElement, $"{expectedElement.name} is null");
            if (expectedElement.name != expectedElementName)
                throw new Exception(
                    $"Are you sure you are passing the correct handle? You pass: {expectedElement.name}. Expected: {expectedElementName}");
        }
    }
}
