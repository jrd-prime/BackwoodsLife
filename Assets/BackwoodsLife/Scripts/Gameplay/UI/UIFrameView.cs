using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Gameplay.UI
{
    public abstract class UIFrameView<TEnum> : MonoBehaviour where TEnum : Enum
    {
        protected readonly Dictionary<TEnum, VisualElement> SubFrames = new();
        protected VisualElement Root { get; private set; }

        public virtual VisualElement GetSubFrame1(TEnum subFrame)
        {
            if (!SubFrames.TryGetValue((TEnum)subFrame, out var frame))
            {
                throw new KeyNotFoundException("No subframe " + subFrame);
            }

            return frame;
        }

        protected void Awake()
        {
            Debug.LogWarning("UIFrameView awake abstract");
            Root = GetComponent<UIDocument>().rootVisualElement;
            Root.ToAbsolute();
            Root.ToSafeArea();
            InitializeSubFramesReference();
            Hide();
        }

        protected abstract void InitializeSubFramesReference();

        public virtual void Show()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public virtual void Hide()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        private void Start()
        {
            if (SubFrames == null || SubFrames.Count == 0) throw new Exception("Subframes not initialized. " + this);
        }
    }
}
