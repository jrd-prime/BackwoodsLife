using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class MainFrameUI : UIFrameView<EMainSubFrame>, IUIFrame
    {
        private VisualElement _root;

        private const string LeftFrameName = "left";

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            Assert.IsNotNull(_root, "root is null");
            _root.ToAbsolute();
            _root.ToSafeArea();
            Hide();

            InitSubFramesDict();
        }

        public VisualElement GetSubFrame(Enum subFrame)
        {
            if (!SubFrames.TryGetValue((EMainSubFrame)subFrame, out var frame))
            {
                throw new KeyNotFoundException("No subframe " + subFrame);
            }

            return frame;
        }

        protected override void InitSubFramesDict()
        {
            SubFrames.TryAdd(EMainSubFrame.Left, _root.Q<VisualElement>(LeftFrameName));
        }

        public override void Show()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public override void Hide()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }
}
