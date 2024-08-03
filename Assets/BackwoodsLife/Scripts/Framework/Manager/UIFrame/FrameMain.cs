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
    public class FrameMain : UIFrameView<EMainSubFrame>, IUIFrame
    {
        private const string TopFrameName = "top";
        private const string LeftFrameName = "left";
        private const string RightFrameName = "right";

        protected override void InitializeSubFramesReference()
        {
            SubFrames[EMainSubFrame.Top] = Root.Q<VisualElement>(TopFrameName);
            SubFrames[EMainSubFrame.Left] = Root.Q<VisualElement>(LeftFrameName);
            SubFrames[EMainSubFrame.Right] = Root.Q<VisualElement>(RightFrameName);

            foreach (var subFrame in SubFrames)
            {
                subFrame.Value.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.0f));
            }
        }

        public void ShowIn(EMainSubFrame subFrameName, in TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame1(subFrameName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(EMainSubFrame subFrameName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame1(subFrameName);

            subFrame.Remove(buildingPanel);
        }
    }
}
