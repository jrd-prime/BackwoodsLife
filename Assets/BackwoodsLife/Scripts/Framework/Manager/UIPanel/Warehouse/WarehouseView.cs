using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Structs;
using BackwoodsLife.Scripts.Data.UI;
using BackwoodsLife.Scripts.Gameplay.UI;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel.Warehouse
{
    public class WarehouseView : UIView
    {
        [SerializeField] private VisualTreeAsset itemViewTemplate;
        private WarehouseViewModel _viewModel;
        private VisualElement _root;
        private readonly CompositeDisposable _disposables = new();
        private VisualElement _container;

        private readonly Dictionary<string, int> _elementsPosition = new();

        [Inject]
        private void Construct(WarehouseViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            Assert.IsNotNull(itemViewTemplate, "item view template is null " + name);
            _root = GetComponent<UIDocument>().rootVisualElement;

            _container = _root.Q<VisualElement>(WarehouseConst.InventoryHUDContainer);
            _container.Clear();

            InitializeView();
            Subscribe();
        }

        private void Subscribe()
        {
            _viewModel.inventoryDataChanged
                .Skip(1)
                .Subscribe(ElementsChanged)
                .AddTo(_disposables);
        }

        private void ElementsChanged(List<InventoryElement> changedElements)
        {
            foreach (var q in changedElements)
            {
                var item = _container.ElementAt(_elementsPosition[q.typeName]);
                item.Q<Label>(WarehouseConst.InventoryHUDItemLabel).text = q.Amount.ToString();
            }
        }

        private async void InitializeView()
        {
            var itemsForInventory = _viewModel.GetInventoryData();
            var i = 0;

            foreach (var t in itemsForInventory)
            {
                Debug.Log($"Add {t} to position {i}");

                var icon = await _viewModel.GetIcon(t.Key);
                var newItem = itemViewTemplate.Instantiate();

                newItem.Q<Label>(WarehouseConst.InventoryHUDItemLabelId).text = t.Key;
                newItem.Q<Label>(WarehouseConst.InventoryHUDItemLabel).text = 0.ToString();
                newItem.Q<VisualElement>(WarehouseConst.InventoryHUDItemIcon).style.backgroundImage = icon.texture;
                _container.Add(newItem);

                _elementsPosition.Add(t.Key, i++);
            }
        }
    }
}
