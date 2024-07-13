using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.Inventory
{
    public class InventoryView : UIView
    {
        [SerializeField] private VisualTreeAsset itemViewTemplate;
        private InventoryViewModel _viewModel;
        private VisualElement _root;
        private readonly CompositeDisposable _disposables = new();
        private VisualElement _container;

        private readonly Dictionary<Enum, int> _elementsPosition = new();

        [Inject]
        private void Construct(InventoryViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            Assert.IsNotNull(itemViewTemplate, "item view template is null " + name);
            _root = GetComponent<UIDocument>().rootVisualElement;
            Debug.LogWarning($"inventory {_viewModel}");

            _container = _root.Q<VisualElement>(InventoryConst.InventoryHUDContainer);
            _container.Clear();

            InitializeView();
            Subscribe();
        }

        private void Subscribe()
        {
            _viewModel.inventoryDataChanged
                .Subscribe(e => ElementsChanged(e))
                .AddTo(_disposables);
        }

        private void ElementsChanged(List<InventoryElement> changedElements)
        {
            Debug.LogWarning($"ElementsChanged {changedElements.Count}");
            foreach (var q in changedElements)
            {
                Debug.LogWarning($"ElementsChanged {q.Type} {q.Amount}");

                var item = _container.ElementAt(_elementsPosition[q.Type]);
                item.Q<Label>(InventoryConst.InventoryHUDItemLabel).text = q.Amount.ToString();
            }
        }

        private void InitializeView()
        {
            var i = 0;
            foreach (EResourceType t in Enum.GetValues(typeof(EResourceType)))
            {
                if (t is EResourceType.None) continue;

                Debug.LogWarning($"Add {t} to position {i}");


                var newItem = itemViewTemplate.Instantiate();
                newItem.Q<Label>(InventoryConst.InventoryHUDItemLabelId).text = t.ToString();
                newItem.Q<Label>(InventoryConst.InventoryHUDItemLabel).text = 0.ToString();
                _container.Add(newItem);


                _elementsPosition.Add(t, i++);
            }
        }
    }
}
