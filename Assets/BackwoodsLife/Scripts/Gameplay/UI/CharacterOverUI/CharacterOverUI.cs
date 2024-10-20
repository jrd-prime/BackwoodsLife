﻿using System.Collections.Generic;
using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI
{
    /// <summary>
    /// Находится на холдере персонажа
    /// </summary>
    public class CharacterOverUI : UIView
    {
        [SerializeField] private GameObject textObjectTemplate;
        private PlayerModel _playerModel;
        private readonly CompositeDisposable _disposables = new();
        private IAssetProvider _assetProvider;

        private List<GameObject> tempObjects = new List<GameObject>();

        [Inject]
        private void Construct(PlayerModel playerModel, IAssetProvider assetProvider)
        {
            _playerModel = playerModel;
            _assetProvider = assetProvider;
        }

        private void Awake()
        {
            Assert.IsNotNull(_playerModel, "_playerModel is null");
            Assert.IsNotNull(textObjectTemplate, "textObjectTemplate is null");
            _playerModel.Position.Subscribe(pos => { transform.position = pos; }).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public async void ShowPopUpFor(List<ItemDto> inventoryElements)
        {
            foreach (var element in inventoryElements)
            {
                // TODO pool
                var inst = Instantiate(textObjectTemplate, parent: transform);

                inst.transform.localScale = Vector3.zero;

                var text = inst.GetComponentInChildren<TMP_Text>();
                var icon = inst.GetComponentInChildren<Image>();

                var iconSprite = await _assetProvider.LoadIconAsync(element.Name);

                if (iconSprite == null) Debug.LogError("We need icon for " + element.Name);


                text.text = $"+ {element.Quantity}";
                icon.sprite = iconSprite;

                inst.transform.DOScale(new Vector3(.7f, .7f, .7f), .7f).SetEase(Ease.Flash);

                inst.transform
                    .DOMoveY(3f, 1f)
                    .SetEase(Ease.InOutSine)
                    .onComplete += () => { Destroy(inst); };
            }
        }

        public async void ShowPopUpForNotEnoughRequirements(List<ItemDto> notEnoughRequirements)
        {
            foreach (var element in notEnoughRequirements)
            {
                // TODO pool
                var inst = Instantiate(textObjectTemplate, parent: transform);
                tempObjects.Add(inst);

                inst.transform.localScale = Vector3.zero;

                var text = inst.GetComponentInChildren<TMP_Text>();
                var icon = inst.GetComponentInChildren<Image>();

                var iconSprite = await _assetProvider.LoadIconAsync(element.Name);

                if (iconSprite == null) Debug.LogError("We need icon for " + element.Name);


                text.text = $"Required: {element.Quantity}";
                icon.sprite = iconSprite;

                inst.transform.localScale = Vector3.one;

                // inst.transform.DOScale(new Vector3(.7f, .7f, .7f), .7f).SetEase(Ease.Flash);

                // inst.transform
                //     .DOMoveY(3f, 1f)
                //     .SetEase(Ease.InOutSine)
                //     .onComplete += () => { Destroy(inst); };
            }
        }

        public void HideNotEnoughPopUp()
        {
            tempObjects.ForEach(x => Destroy(x));
        }
    }
}
