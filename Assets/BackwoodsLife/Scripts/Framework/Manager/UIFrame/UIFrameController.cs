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
        private Button _closeBtn;

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
        public FramePopUpWindow GetPopUpWindowFrame() => framePopUpWindow;

        public void ShowMainPopUpWindowWithScroll(TemplateContainer instantiate)
        {
            var root = framePopUpWindow.GetSubFrame1(EMainPopUpSubFrame.Root);
            Debug.LogWarning(root);
            var fp = framePopUpWindow.GetSubFrame1(EMainPopUpSubFrame.Main);

            _closeBtn = root.Q<Button>("close");

            _closeBtn.clicked += CloseMainPopUpWindow;

            fp.Add(instantiate);
            framePopUpWindow.Show();
        }

        private void CloseMainPopUpWindow()
        {
            framePopUpWindow.Hide();
            _closeBtn.clicked -= CloseMainPopUpWindow;
        }

        public void ShowMainPopUpWindow(TemplateContainer instantiate)
        {
            var fp = framePopUpWindow.GetSubFrame1(EMainPopUpSubFrame.Description);
            var ap = fp.Q<VisualElement>("in-window-container");

            _closeBtn = fp.Q<Button>("close");

            _closeBtn.clicked += CloseMainPopUpWindow;

            ap.Add(instantiate);
            framePopUpWindow.Show();
        }
    }
}
