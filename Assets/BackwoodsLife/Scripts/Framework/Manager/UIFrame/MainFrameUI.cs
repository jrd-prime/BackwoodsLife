using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class MainFrameUI : UIFrameView<EMainSubFrame>, IUIFrame
    {
        private VisualElement _root;

        private const string TopFrameName = "top";
        private const string LeftFrameName = "left";
        private const string RightFrameName = "right";

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            Assert.IsNotNull(_root, "root is null");
            _root.ToAbsolute();
            _root.ToSafeArea();

            SubFrames[EMainSubFrame.Top] = _root.Q<VisualElement>(TopFrameName);
            SubFrames[EMainSubFrame.Left] = _root.Q<VisualElement>(LeftFrameName);
            SubFrames[EMainSubFrame.Right] = _root.Q<VisualElement>(RightFrameName);

            foreach (var subFrame in SubFrames)
            {
                subFrame.Value.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.0f));
            }
            // Hide();
        }

        public VisualElement GetSubFrame(Enum subFrame)
        {
            if (!SubFrames.TryGetValue((EMainSubFrame)subFrame, out var frame))
            {
                throw new KeyNotFoundException("No subframe " + subFrame);
            }

            return frame;
        }

        public override void Show()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public override void Hide()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        public void ShowIn(EMainSubFrame subFrameName, in TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame(subFrameName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(EMainSubFrame subFrameName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame(subFrameName);

            subFrame.Remove(buildingPanel);
        }
    }
}
