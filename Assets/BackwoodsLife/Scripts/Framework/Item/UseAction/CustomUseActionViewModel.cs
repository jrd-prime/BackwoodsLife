using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;
using BackwoodsLife.Scripts.Framework.Item.UseAction.sort;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionViewModelBase : IUseActionViewModel
    {
        public ReactiveProperty<int> Show = new();
        public abstract string Description { get; }
        public abstract void Activate(Action onCompleteUseActionCallback);
        public abstract void Deactivate();

        public static IUseActionViewModel CreateUseAction(EUseType useType)
        {
            return useType switch
            {
                // EUseType.Cooking => new CookingActionViewModel(),
                // EUseType.PutItems => new PutActionViewModel(),
                // EUseType.TakeItems => new TakeActionViewModel(),
                EUseType.Crafting => new CraftingViewModel(),
                // EUseType.Resting => new RestingActionViewModel(),
                // EUseType.Fishing => new FishingActionViewModel(),
                // EUseType.Drinking => new DrinkingActionViewModel(),
                // EUseType.Eating => new EatingActionViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(useType), useType,
                    "Unknown use type! Need to add new use type")
            };
        }
    }

    public abstract class CustomUseActionViewModel<TModel> : UseActionViewModelBase
        where TModel : class, IUseActionModel
    {
        public TModel Model { get; private set; }

        [Inject]
        private void Construct(TModel model)
        {
            Debug.LogWarning("Use action view model construct");
            Model = model;
        }
    }
}
