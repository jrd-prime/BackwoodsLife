using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class FramePopUp : UIFrameView<PopUpSubFrameType>, IUIFrame
    {
        private const string LeftFrameName = "left-frame";
        private const string CenterFrameName = "center-frame";
        private const string RightFrameName = "right-frame";

        protected override void InitializeSubFramesReference()
        {
            SubFrames[PopUpSubFrameType.Left] = Root.Q<VisualElement>(LeftFrameName);
            SubFrames[PopUpSubFrameType.Center] = Root.Q<VisualElement>(CenterFrameName);
            SubFrames[PopUpSubFrameType.Right] = Root.Q<VisualElement>(RightFrameName);
        }


        public override void Show()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public override void Hide()
        {
            Root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        public void ShowIn(PopUpSubFrameType subFrameTypeName, in TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame1(subFrameTypeName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(PopUpSubFrameType subFrameTypeName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame1(subFrameTypeName);

            subFrame.Remove(buildingPanel);
        }
    }
}
