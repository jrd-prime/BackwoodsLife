using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame

{
    [RequireComponent(typeof(UIDocument))]
    public class UIFrameController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset uiFrameAsset;
        [SerializeField] private FrameMain frameMainAsset;
        [SerializeField] private FramePopUp framePopUpAsset;
        [SerializeField] private FramePopUpWindow framePopUpWindow;

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

        public void ShowMainPopUpWindow(TemplateContainer instantiate)
        {
            var fp = framePopUpWindow.GetSubFrame1(EMainPopUpSubFrame.Full);
            var ap = fp.Q<VisualElement>("in-window-container");

            ap.Add(instantiate);
            framePopUpWindow.Show();
        }
    }
}
