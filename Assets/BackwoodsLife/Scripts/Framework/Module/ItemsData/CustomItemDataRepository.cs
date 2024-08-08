using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Framework.Manager.UIFrame.UIButtons;
using R3;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Module.ItemsData
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

        public void Increase(string res, int amount) => Model.AddItem(res, amount);
        public void Increase(in List<ItemData> inventoryElements) => Model.AddItem(inventoryElements);
        public void DecreaseResource(string objResourceType, int amount) => Model.RemoveItem(objResourceType, amount);
        public void DecreaseResource(in List<ItemData> inventoryElements) => Model.RemoveItem(inventoryElements);

        public void Initialize()
        {
            Assert.IsNotNull(Model, "data model is null");
            // Assert.IsNotNull(_uiButtonsController, "UiButtonsController is null");

            Model.SetItemsToInitialization(GetOnLoadItemsAndValues());
        }

        protected abstract Dictionary<string, int> GetOnLoadItemsAndValues();
    }
}
