using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class FramePopUpWindow : UIFrameView<EMainPopUpSubFrame>, IUIFrame
    {
        private Label title;

        protected override void InitializeSubFramesReference()
        {
            var desc = Root.Q<VisualElement>("desc-container");
            var main = Root.Q<VisualElement>("main-container");

            Assert.IsNotNull(desc, "Desc container in FramePopUpWindow is null");
            Assert.IsNotNull(main, "Main container in FramePopUpWindow is null");

            SubFrames[EMainPopUpSubFrame.Root] = Root;
            SubFrames[EMainPopUpSubFrame.Description] = desc;
            SubFrames[EMainPopUpSubFrame.Main] = main;


            title = desc.Q<Label>("head-label");
        }


        public void SetDescription(PanelDescriptionData panelDescriptionData)
        {
            title.text = panelDescriptionData.Title;
        }
    }
}
