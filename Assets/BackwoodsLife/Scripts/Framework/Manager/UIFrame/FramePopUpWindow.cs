using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class FramePopUpWindow : UIFrameView<EMainPopUpSubFrame>, IUIFrame
    {
        [SerializeField] private VisualTreeAsset mainPopUpWindowAsset;

        protected override void InitializeSubFramesReference()
        {
            SubFrames[EMainPopUpSubFrame.Full] = Root.Q<VisualElement>("full");
        }
    }
}
