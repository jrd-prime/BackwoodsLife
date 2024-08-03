using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI
{
    public abstract class UIFrameView<TEnum> : MonoBehaviour where TEnum : Enum
    {
        protected readonly Dictionary<TEnum, VisualElement> SubFrames = new();
        public abstract void Show();
        public abstract void Hide();

        private void Start()
        {
            if (SubFrames == null || SubFrames.Count == 0) throw new Exception("Subframes not initialized. " + this);
        }
    }
}
