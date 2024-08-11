using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Item.DataModel;
using BackwoodsLife.Scripts.Framework.Item.DataModel.Warehouse;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.Warehouse
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

        private void ElementsChanged(List<ItemDataChanged> changedElements)
        {
            foreach (var q in changedElements)
            {
                //TODO anim from to
                var item = _container.ElementAt(_elementsPosition[q.Name]);
                item.Q<Label>(WarehouseConst.InventoryHUDItemLabel).text = q.To.ToString();
            }
        }

        private async void InitializeView()
        {
            var i = 0;
            foreach (var t in _viewModel.GetInventoryData())
            {
                Debug.Log($"Add {t} to position {i}");

                var icon = await _viewModel.GetIcon(t.Key); // TODO переделать
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
