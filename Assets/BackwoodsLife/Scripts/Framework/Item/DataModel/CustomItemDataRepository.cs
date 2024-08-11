using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Gameplay.UI.UIButtons;
using R3;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.DataModel
{
    public abstract class CustomItemDataRepository<TItemDataModel> : IItemDataManager
        where TItemDataModel : class, IItemDataRepository
    {
        protected IItemDataRepository Model;
        protected readonly CompositeDisposable Disposable = new();

        [Inject]
        private void Construct(TItemDataModel model, UIButtonsController uiButtonsController)
        {
            Model = model;
            // _uiButtonsController = uiButtonsController;
        }

        public bool Increase(string res, int amount) => Model.AddItem(res, amount);
        public bool Increase(in List<ItemData> inventoryElements) => Model.AddItem(inventoryElements);
        public bool DecreaseResource(string objResourceType, int amount) => Model.RemoveItem(objResourceType, amount);
        public bool DecreaseResource(in List<ItemData> inventoryElements) => Model.RemoveItem(inventoryElements);

        public void Initialize()
        {
            Assert.IsNotNull(Model, "data model is null");
            // Assert.IsNotNull(_uiButtonsController, "UiButtonsController is null");

            Model.SetItemsToInitialization(GetOnLoadItemsAndValues());
        }


        public IReadOnlyDictionary<string, int> GetCacheData() => Model.GetCacheData();


        protected abstract Dictionary<string, int> GetOnLoadItemsAndValues();
    }
}
