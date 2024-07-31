using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class PopUpFrameUI : UIFrameView<EPopUpSubFrame>, IUIFrame
    {
        private VisualElement _root;

        private const string LeftFrameName = "left-frame";
        private const string CenterFrameName = "center-frame";
        private const string RightFrameName = "right-frame";

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.ToAbsolute();
            _root.ToSafeArea();
            Hide();

            InitSubFramesDict();
        }

        protected override void InitSubFramesDict()
        {
            SubFrames[EPopUpSubFrame.Left] = _root.Q<VisualElement>(LeftFrameName);
            SubFrames[EPopUpSubFrame.Center] = _root.Q<VisualElement>(CenterFrameName);
            SubFrames[EPopUpSubFrame.Right] = _root.Q<VisualElement>(RightFrameName);
        }

        public override void Show()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public override void Hide()
        {
            _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        public VisualElement GetSubFrame(Enum subFrame)
        {
            if (!SubFrames.TryGetValue((EPopUpSubFrame)subFrame, out var frame))
            {
                throw new KeyNotFoundException("No subframe " + subFrame);
            }

            return frame;
        }

        public void ShowIn(EPopUpSubFrame subFrameName, ref TemplateContainer visualTemplate, bool clear = true)
        {
            var subFrame = GetSubFrame(subFrameName);
            if (clear) subFrame.Clear();

            subFrame.Add(visualTemplate);

            Show();
        }

        public void HideIn(EPopUpSubFrame subFrameName, in TemplateContainer buildingPanel)
        {
            var subFrame = GetSubFrame(subFrameName);

            subFrame.Remove(buildingPanel);
        }
    }
}
