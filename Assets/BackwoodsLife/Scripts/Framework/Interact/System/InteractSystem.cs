using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.Unit;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Interact.System
{
    /// <summary>
    /// Placed on character prefab
    /// </summary>
    public class InteractSystem : MonoBehaviour
    {
        [SerializeField] private GameObject textObjectTemplate;

        private CollectSystem _collectSystem;
        private PlayerModel _playerModel;
        private IInteractableSystem _usableSystem;
        private IInteractableSystem _upgradableSystem;
        private IInteractableSystem _usableAndUpgradableSystem;
        private CharacterOverUI _characterOverUIHolder;

        public event Action<List<CollectableElement>> OnCollected;


        [Inject]
        private void Construct(PlayerModel playerModel, CollectSystem collectSystem,
            CharacterOverUI characterOverUIHolder)
        {
            _playerModel = playerModel;
            _collectSystem = collectSystem;
            _characterOverUIHolder = characterOverUIHolder;
        }

        private void Awake()
        {
            Assert.IsNotNull(textObjectTemplate, "textObjectTemplate is null");
            OnCollected += OnCollect;
        }


        private void OnCollect(List<CollectableElement> obj)
        {
            foreach (var element in obj)
            {
                // TODO pool
                var inst = Instantiate(textObjectTemplate, parent: _characterOverUIHolder.transform);

                inst.transform.localScale = Vector3.zero;

                var t = inst.GetComponentInChildren<TMP_Text>();


                t.text = $"+ {RandomCollector.GetRandom(element.Range.min, element.Range.max)} {element.Name}";

                inst.transform.DOScale(new Vector3(.7f, .7f, .7f), .7f).SetEase(Ease.Flash);

                inst.transform
                    .DOMoveY(3f, 1f)
                    .SetEase(Ease.InOutSine)
                    .onComplete += () => { Destroy(inst); };
            }
        }

        private void OnUseAndUpgrade(List<CollectableElement> collectableElements)
        {
            throw new NotImplementedException();
        }

        private void OnUpgrade(List<CollectableElement> collectableElements)
        {
            throw new NotImplementedException();
        }

        private void OnUse(List<CollectableElement> collectableElements)
        {
            throw new NotImplementedException();
        }


        public void Interact(ref InteractableObject interactableObject)
        {
            if (interactableObject == null) throw new NullReferenceException("Interactable obj is null");

            switch (interactableObject.data.interactableType)
            {
                case EInteractableObject.Default:
                    throw new Exception("Interactable type not set. " + interactableObject.name);
                case EInteractableObject.Collectable:
                    interactableObject.Process(_collectSystem, OnCollected);
                    break;
                case EInteractableObject.Usable:
                    interactableObject.Process(_usableSystem, OnUse);
                    break;
                case EInteractableObject.Upgradable:
                    interactableObject.Process(_upgradableSystem, OnUpgrade);
                    break;
                case EInteractableObject.UsableAndUpgradable:
                    interactableObject.Process(_usableAndUpgradableSystem, OnUseAndUpgrade);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
