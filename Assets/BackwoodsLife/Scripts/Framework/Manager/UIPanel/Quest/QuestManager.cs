using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using CompositeDisposable = R3.CompositeDisposable;

namespace BackwoodsLife.Scripts.Framework.Manager.Quest
{
    public class QuestManager : IInitializable
    {
        private UIButtonsController _uiButtonsController;
        private readonly CompositeDisposable _disposable = new();
        private UIFrameController _uiFrameController;

        [Inject]
        private void Construct(UIFrameController uiFrameController, UIButtonsController uiButtonsController)
        {
            _uiButtonsController = uiButtonsController;
            _uiFrameController = uiFrameController;
        }


        public void Initialize()
        {
            _uiButtonsController.QuestButtonClicked
                .Subscribe(_ => { QuestButtonClicked(); })
                .AddTo(_disposable);
        }

        private void QuestButtonClicked()
        {
            Debug.LogWarning("Quest clicked");
            _uiFrameController.ShowMainPopUpWindow();
        }
    }
}
