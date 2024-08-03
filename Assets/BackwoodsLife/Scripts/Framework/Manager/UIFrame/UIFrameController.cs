using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame

{
    [RequireComponent(typeof(UIDocument))]
    public class UIFrameController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiFrameAsset;

        [FormerlySerializedAs("mainFrameUIAsset")] [SerializeField]
        private FrameMain frameMainAsset;

        [FormerlySerializedAs("popUpFrameUIAsset")] [SerializeField]
        private FramePopUp framePopUpAsset;

        [FormerlySerializedAs("mainPopUpWindow")] [SerializeField]
        private FramePopUpWindow framePopUpWindow;

        private VisualElement _mu;
        private VisualElement _pu;
        private UIDocument uiDocument;
        private VisualElement _root;

        private void Awake()
        {
            Assert.IsNotNull(uiFrameAsset, "uiFrame is null");
            Assert.IsNotNull(frameMainAsset, "mainUi is null");
            Assert.IsNotNull(framePopUpAsset, "popUpUi is null");

            _root = GetComponent<UIDocument>().rootVisualElement;
            uiDocument = GetComponent<UIDocument>();
        }


        public VisualElement GetMainUI() => _mu;

        public VisualElement GetPopUpUILeft() => _pu.Q<VisualElement>("left-frame");

        public void ShowPopUpUi() => _pu.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

        // TODO generic
        public IUIFrame GetFrame(EUIFrame frame)
        {
            return frame switch
            {
                EUIFrame.Main => frameMainAsset,
                EUIFrame.PopUp => framePopUpAsset,
                EUIFrame.MainPopUpWindow => framePopUpWindow,
                _ => throw new ArgumentOutOfRangeException(nameof(frame), frame, null)
            };
        }

        public FramePopUp GetPopUpFrame() => framePopUpAsset;
        public FrameMain GetMainFrame() => frameMainAsset;

        public void ShowMainPopUpWindow()
        {
            framePopUpWindow.Show();
        }
    }
}
