using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class UIFrameController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiFrameAsset;
        [SerializeField] private MainFrameUI mainFrameUIAsset;
        [SerializeField] private PopUpFrameUI popUpFrameUIAsset;

        private VisualElement _mu;
        private VisualElement _pu;

        private void Awake()
        {
            Assert.IsNotNull(uiFrameAsset, "uiFrame is null");
            Assert.IsNotNull(mainFrameUIAsset, "mainUi is null");
            Assert.IsNotNull(popUpFrameUIAsset, "popUpUi is null");
        }


        public VisualElement GetMainUI() => _mu;

        public VisualElement GetPopUpUILeft() => _pu.Q<VisualElement>("left-frame");

        public void ShowPopUpUi() => _pu.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

        // TODO generic
        public IUIFrame GetFrame(EUIFrame frame)
        {
            return frame switch
            {
                EUIFrame.Main => mainFrameUIAsset,
                EUIFrame.PopUp => popUpFrameUIAsset,
                _ => throw new ArgumentOutOfRangeException(nameof(frame), frame, null)
            };
        }

        public PopUpFrameUI GetPopUpFrame() => popUpFrameUIAsset;
        public MainFrameUI GetMainFrame() => mainFrameUIAsset;
    }
}
