using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class FrameMain : UIFrameView<MainSubFrameType>, IUIFrame
    {
        private const string TopFrameName = "top";
        private const string LeftFrameName = "left";
        private const string RightFrameName = "right";

        protected override void InitializeSubFramesReference()
        {
            SubFrames[MainSubFrameType.Top] = Root.Q<VisualElement>(TopFrameName);
            SubFrames[MainSubFrameType.Left] = Root.Q<VisualElement>(LeftFrameName);
            SubFrames[MainSubFrameType.Right] = Root.Q<VisualElement>(RightFrameName);

            foreach (var subFrame in SubFrames)
            {
                subFrame.Value.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.0f));
            }
        }

        public void ShowIn(MainSubFrameType subFrameTypeName, in TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame1(subFrameTypeName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(MainSubFrameType subFrameTypeName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame1(subFrameTypeName);

            subFrame.Remove(buildingPanel);
        }
    }
}
