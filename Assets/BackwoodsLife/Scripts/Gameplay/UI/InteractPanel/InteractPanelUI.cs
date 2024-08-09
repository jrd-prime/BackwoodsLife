using System;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.InteractPanel
{
    /// <summary>
    /// Находится на холдере персонажа
    /// </summary>
    public class InteractPanelUI : UIView
    {
        [SerializeField] private StyleSheet styleSheet;
        [SerializeField] private VisualTreeAsset buttonTemplate;
        [SerializeField] private VisualTreeAsset buildButtonTemplate;
        private PlayerModel _playerModel;
        private readonly CompositeDisposable _disposables = new();
        private IAssetProvider _assetProvider;
        private VisualElement root;
        private VisualElement interactPanel;
        private VisualElement leftFrame;

        private Button _buildButton;
        private UIFrameController _uiFrameController;


        [Inject]
        private void Construct(PlayerModel playerModel, IAssetProvider assetProvider,
            UIFrameController uiFrameController)
        {
            _playerModel = playerModel;
            _assetProvider = assetProvider;
            _uiFrameController = uiFrameController;
        }

        private void Awake()
        {
            Assert.IsNotNull(_playerModel, "_playerModel is null");
            Assert.IsNotNull(buttonTemplate, "buttonTemplate is null");
            Assert.IsNotNull(buildButtonTemplate, "buttonTemplate is null");

            // root = GetComponent<UIDocument>().rootVisualElement;
            // interactPanel = root.Q<VisualElement>("main-popup-frame");
            // leftFrame = interactPanel.Q<VisualElement>("left-frame");
            // root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

            _playerModel.Position.Subscribe(pos => { transform.position = pos; }).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        // public async void ShowPopUpFor(List<InventoryElement> inventoryElements)
        // {
        //     foreach (var element in inventoryElements)
        //     {
        //         // TODO pool
        //         var inst = Instantiate(buttonTemplate, parent: transform);
        //
        //         inst.transform.localScale = Vector3.zero;
        //
        //         var text = inst.GetComponentInChildren<TMP_Text>();
        //         var icon = inst.GetComponentInChildren<Image>();
        //
        //         var iconSprite = await _assetProvider.LoadIconAsync(element.typeName);
        //
        //         if (iconSprite == null) Debug.LogError("We need icon for " + element.typeName);
        //
        //
        //         text.text = $"+ {element.Amount}";
        //         icon.sprite = iconSprite;
        //
        //         inst.transform.DOScale(new Vector3(.7f, .7f, .7f), .7f).SetEase(Ease.Flash);
        //
        //         inst.transform
        //             .DOMoveY(3f, 1f)
        //             .SetEase(Ease.InOutSine)
        //             .onComplete += () => { Destroy(inst); };
        //     }
        // }

        public void Show(EInteractTypes interactTypes)
        {
            switch (interactTypes)
            {
                case EInteractTypes.Collect:
                    break;
                case EInteractTypes.Use:
                    break;
                case EInteractTypes.Upgrade:
                    break;
                case EInteractTypes.UseAndUpgrade:

                    var useb = buttonTemplate.Instantiate();
                    useb.Q<Button>("ip-button").text = "Use"; // TODO конкретное действие

                    var upgb = buttonTemplate.Instantiate();
                    upgb.Q<Button>("ip-button").text = "Upgrade"; // TODO конкретное действие

                    root.Q<VisualElement>("interact-panel").Add(useb);
                    root.Q<VisualElement>("interact-panel").Add(upgb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interactTypes), interactTypes, null);
            }

            root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
    }
}
