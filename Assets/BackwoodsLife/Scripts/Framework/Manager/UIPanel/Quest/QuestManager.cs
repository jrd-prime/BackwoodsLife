using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;
using CompositeDisposable = R3.CompositeDisposable;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.Quest
{
    public class QuestManager : IInitializable
    {
        private UIButtonsController _uiButtonsController;
        private readonly CompositeDisposable _disposable = new();
        private UIFrameController _uiFrameController;
        private PanelUIController _panelUIController;

        public List<QuestConfig> Quests { get; set; } = new(); //TODO remove>

        [Inject]
        private void Construct(UIFrameController uiFrameController, UIButtonsController uiButtonsController,
            PanelUIController panelUIController)
        {
            _uiButtonsController = uiButtonsController;
            _uiFrameController = uiFrameController;
            _panelUIController = panelUIController;
        }


        public void Initialize()
        {
            Quests.Add(new QuestConfig { id = 1, title = "Build: Bonfire", });
            Quests.Add(new QuestConfig { id = 2, title = "Build: House", });
            Quests.Add(new QuestConfig { id = 3, title = "Craft: Stone knife", });
            Quests.Add(new QuestConfig { id = 4, title = "Craft: Basket", });
            Quests.Add(new QuestConfig { id = 5, title = "Collect: Wildberry. Req: Basket", });
            Quests.Add(new QuestConfig { id = 6, title = "Craft: Stone axe", });
            Quests.Add(new QuestConfig { id = 7, title = "Collect: Wood. Req: Stone axe", });
            Quests.Add(new QuestConfig { id = 8, title = "Add wood to bonfire", });
            Quests.Add(new QuestConfig { id = 9, title = "Build: Warhouse", });
            Quests.Add(new QuestConfig { id = 10, title = "Drink water in river", });
            Quests.Add(new QuestConfig { id = 11, title = "Craft: Fish trap", });
            Quests.Add(new QuestConfig { id = 12, title = "Place: Fish trap", });
            Quests.Add(new QuestConfig { id = 13, title = "Collect: Fish", });
            Quests.Add(new QuestConfig { id = 14, title = "Place: Fish to bonfire", });
            Quests.Add(new QuestConfig { id = 15, title = "Collect: Cooked fish ", });
            Quests.Add(new QuestConfig { id = 16, title = "Eat cooked fish", });
            Quests.Add(new QuestConfig { id = 17, title = "Sleep - energy restore x2", });


            _uiButtonsController.QuestButtonClicked
                .Subscribe(_ => { QuestButtonClicked(); })
                .AddTo(_disposable);
        }

        private void QuestButtonClicked()
        {
            Debug.LogWarning("Quest clicked");


            _uiFrameController.ShowMainPopUpWindow(FillInWindowQuests());
        }

        private TemplateContainer FillInWindowQuests()
        {
            var controller = _panelUIController.GetController<QuestPanelUIController>();

            var uiTemplate = controller.GetTemplateFor("InWindow");
            var templateContainer = uiTemplate.Instantiate();

            templateContainer.ToAbsolute();


            var questItemTemplate = controller.GetTemplateFor("QuestItem");
            var scrollView = templateContainer.Q<ScrollView>("iwp-scroll-view")
                .Q<VisualElement>("unity-content-container");

            foreach (var questConfig in Quests)
            {
                var questItem = questItemTemplate.Instantiate();

                questItem.Q<Label>("qpiw-head-label").text = questConfig.title;
                questItem.Q<Label>("qpiw-description-label").text = questConfig.description;
                scrollView.Add(questItem);
            }


            return templateContainer;
        }
    }

}
