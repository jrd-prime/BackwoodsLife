using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.Inventory;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
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

        private readonly Dictionary<string, int> _elementsPosition = new();
        private readonly Dictionary<string, Sprite> _iconsCache = new();
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(InventoryViewModel viewModel, IAssetProvider assetProvider)
        {
            _viewModel = viewModel;
            _assetProvider = assetProvider;
        }

        private void Awake()
        {
            Assert.IsNotNull(itemViewTemplate, "item view template is null " + name);
            _root = GetComponent<UIDocument>().rootVisualElement;

            _container = _root.Q<VisualElement>(InventoryConst.InventoryHUDContainer);
            _container.Clear();

            InitializeView();
            Subscribe();
        }

        private void Subscribe()
        {
            _viewModel.inventoryDataChanged
                .Skip(1)
                .Subscribe(e => ElementsChanged(e))
                .AddTo(_disposables);
        }

        private void ElementsChanged(List<InventoryElement> changedElements)
        {
            foreach (var q in changedElements)
            {
                var item = _container.ElementAt(_elementsPosition[q.typeName]);
                item.Q<Label>(InventoryConst.InventoryHUDItemLabel).text = q.Amount.ToString();
            }
        }

        private async void InitializeView()
        {
            var itemsForInventory = _viewModel.GetInventoryData();
            var i = 0;
            foreach (var t in itemsForInventory)
            {
                Debug.LogWarning($"Add {t} to position {i}");
                var icon = await _assetProvider.LoadIconAsync(t.Key.ToString());
                Debug.LogWarning("icon " + icon);

                var newItem = itemViewTemplate.Instantiate();
                newItem.Q<Label>(InventoryConst.InventoryHUDItemLabelId).text = t.Key.ToString();
                newItem.Q<Label>(InventoryConst.InventoryHUDItemLabel).text = 0.ToString();
                newItem.Q<VisualElement>(InventoryConst.InventoryHUDItemIcon).style.backgroundImage = icon.texture;
                _container.Add(newItem);

                _elementsPosition.Add(t.Key, i++);
            }
        }
    }
}
