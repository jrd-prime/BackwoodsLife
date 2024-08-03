using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class FramePopUp : UIFrameView<EPopUpSubFrame>, IUIFrame
    {
        private const string LeftFrameName = "left-frame";
        private const string CenterFrameName = "center-frame";
        private const string RightFrameName = "right-frame";

        protected override void InitializeSubFramesReference()
        {
            SubFrames[EPopUpSubFrame.Left] = Root.Q<VisualElement>(LeftFrameName);
            SubFrames[EPopUpSubFrame.Center] = Root.Q<VisualElement>(CenterFrameName);
            SubFrames[EPopUpSubFrame.Right] = Root.Q<VisualElement>(RightFrameName);
        }


        public override void Show()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public override void Hide()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        public void ShowIn(EPopUpSubFrame subFrameName, in TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame1(subFrameName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(EPopUpSubFrame subFrameName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame1(subFrameName);

            subFrame.Remove(buildingPanel);
        }
    }
}
