using System;
using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
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
        [SerializeField] private FrameMain frameMainAsset;
        [SerializeField] private FramePopUp framePopUpAsset;

        [FormerlySerializedAs("popUpWindowFrame")] [FormerlySerializedAs("framePopUpWindow")] [SerializeField]
        private PopUpWindowFrame popUpWindow;

        public Action OnCloseButtonClicked;

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

        public void ShowPopUpWindow(PanelDescriptionData panelDescriptionData, TemplateContainer filledContent)
        {
            popUpWindow.Show();
        }

        public void HidePopUpWindow() => popUpWindow.Hide();


        public FrameMain GetMainFrame() => frameMainAsset;
        public FramePopUp GetPopUpFrame() => framePopUpAsset;

        public void ShowMainPopUpWindowWithScroll(TemplateContainer instantiate)
        {
            var root = popUpWindow.GetSubFrame1(EMainPopUpSubFrame.Root);
            Debug.LogWarning(root);
            var fp = popUpWindow.GetSubFrame1(EMainPopUpSubFrame.Main);

            _closeBtn = root.Q<Button>("close");

            _closeBtn.clicked += CloseMainPopUpWindow;

            fp.Add(instantiate);
            popUpWindow.Show();
        }

        private void CloseMainPopUpWindow()
        {
            OnCloseButtonClicked.Invoke();
            popUpWindow.Hide();
            _closeBtn.clicked -= CloseMainPopUpWindow;
        }

        public void ShowMainPopUpWindow(TemplateContainer instantiate)
        {
            var fp = popUpWindow.GetSubFrame1(EMainPopUpSubFrame.Description);
            var ap = fp.Q<VisualElement>("in-window-container");

            _closeBtn = fp.Q<Button>("close");

            _closeBtn.clicked += CloseMainPopUpWindow;

            ap.Add(instantiate);
            popUpWindow.Show();
        }

        public void SetDescriptionToPopUpWindow(PanelDescriptionData panelDescriptionData) =>
            popUpWindow.SetDescription(panelDescriptionData);
    }
}
