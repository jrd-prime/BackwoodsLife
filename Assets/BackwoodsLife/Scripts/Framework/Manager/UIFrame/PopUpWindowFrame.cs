﻿using BackwoodsLife.Scripts.Data.Common.Enums.UI;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    /// <summary>
    /// Фрейм, содержащий основное окно в котором есть панели: desc-container и main-container. И кнопку закрытия окна
    /// </summary>
    public class PopUpWindowFrame : UIFrameView<MainPopUpSubFrameType>, IUIFrame
    {
        private Label title;

        protected override void InitializeSubFramesReference()
        {
            var desc = Root.Q<VisualElement>("desc-container");
            var main = Root.Q<VisualElement>("main-container");

            Assert.IsNotNull(desc, "Desc container in FramePopUpWindow is null");
            Assert.IsNotNull(main, "Main container in FramePopUpWindow is null");

            SubFrames[MainPopUpSubFrameType.Root] = Root;
            SubFrames[MainPopUpSubFrameType.Description] = desc;
            SubFrames[MainPopUpSubFrameType.Main] = main;
            
            title = desc.Q<Label>("head-label");
        }


        public void SetDescription(PanelDescriptionData panelDescriptionData)
        {
            title.text = panelDescriptionData.Title;
        }
    }
}
