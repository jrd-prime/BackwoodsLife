using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Provider.AssetProvider;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData.Warehouse
{
    public class WarehouseViewModel : IViewModel
    {
        public ReadOnlyReactiveProperty<List<ItemDataChanged>> inventoryDataChanged => _model.OnRepositoryDataChanged;

        private WarehouseItemDataModel _model;
        private WarehouseManager _manager;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(WarehouseItemDataModel model, WarehouseManager manager, IAssetProvider assetProvider)
        {
            _model = model;
            _manager = manager;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            Assert.IsNotNull(_model, "_model is null");
            Assert.IsNotNull(_manager, "_manager is null");
            Assert.IsNotNull(_assetProvider, "_assetProvider is null");
        }

        public IReadOnlyDictionary<string, int> GetInventoryData() => _manager.GetCacheData();


        public UniTask<Sprite> GetIcon(string tKey)
        {
            return _assetProvider.LoadIconAsync(tKey);
        }
    }
}
