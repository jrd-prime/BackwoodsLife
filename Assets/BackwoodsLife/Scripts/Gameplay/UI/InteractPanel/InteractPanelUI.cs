using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Data.Common.Structs.Required;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using VContainer;
using Image = UnityEngine.UI.Image;

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

        private Button _buildButton;

        [Inject]
        private void Construct(PlayerModel playerModel, IAssetProvider assetProvider)
        {
            _playerModel = playerModel;
            _assetProvider = assetProvider;
        }

        private void Awake()
        {
            Assert.IsNotNull(_playerModel, "_playerModel is null");
            Assert.IsNotNull(buttonTemplate, "buttonTemplate is null");
            Assert.IsNotNull(buildButtonTemplate, "buttonTemplate is null");

            root = GetComponent<UIDocument>().rootVisualElement;
            interactPanel = root.Q<VisualElement>("interact-panel");
            root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);


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

        private Dictionary<Type, Dictionary<Enum, int>> TestReqForUpgrade(UpgradeLevel level)
        {
            var dict = new Dictionary<Type, Dictionary<Enum, int>>();
            AddToDictReqForUpgradeFor(level.requirementsForUpgrading.resource, ref dict);
            AddToDictReqForUpgradeFor(level.requirementsForUpgrading.building, ref dict);
            AddToDictReqForUpgradeFor(level.requirementsForUpgrading.tool, ref dict);
            AddToDictReqForUpgradeFor(level.requirementsForUpgrading.skill, ref dict);
            return dict;
        }

        private void AddToDictReqForUpgradeFor<T>(List<CustomRequirement<T>> res,
            ref Dictionary<Type, Dictionary<Enum, int>> dict) where T : Enum
        {
            var dict2 = res.ToDictionary<CustomRequirement<T>, Enum, int>(re => re.typeName, re => re.value);

            dict.Add(typeof(T), dict2);
        }


        public void ShowPanelForBuild(SWorldItemConfigNew worldItemConfig)
        {
            UpgradeLevel levreq = worldItemConfig.GetLevelRequirements(ELevel.Level_1);

            var a = TestReqForUpgrade(levreq);

            foreach (var d in a)
            {
                Debug.LogWarning($"\t{d.Key}");
                foreach (var i in d.Value)
                {
                    Debug.LogWarning($"\t\t{i.Key} = {i.Value}");
                }
            }

            Debug.LogWarning("lev = " + levreq.level);


            var buildButton = buildButtonTemplate.Instantiate();

            buildButton.Q<Label>("ip-building-name-label").text =
                worldItemConfig.name.ToUpper(); // TODO конкретное действие

            interactPanel.Add(buildButton);

            buildButton.styleSheets.Add(styleSheet);
            buildButton.AddToClassList("ip-build-button-template");
            _buildButton = buildButton.Q<Button>("ip-build-button");
            _buildButton.clicked += OnBuildButtonClicked;

            root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public void HidePanelForBuild()
        {
            _buildButton.clicked -= OnBuildButtonClicked;
            root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            interactPanel.Clear();
        }

        private void OnBuildButtonClicked()
        {
            Debug.LogWarning("Build button click");
        }
    }
}
