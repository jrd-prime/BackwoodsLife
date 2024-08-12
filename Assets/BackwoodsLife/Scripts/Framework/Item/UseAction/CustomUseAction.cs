using System;
using BackwoodsLife.Scripts.Data.Scriptable.Items;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class CustomUseAction : IUseAction
    {
        public abstract string Description { get; }
        public abstract void Activate();
        public abstract void Deactivate();

        public static IUseAction CreateUseAction(EUseType useType)
        {
            return useType switch
            {
                EUseType.Cooking => new CookingAction(),
                EUseType.PutItems => new PutAction(),
                EUseType.TakeItems => new TakeAction(),
                EUseType.Crafting => new CraftingAction(),
                EUseType.Resting => new RestingAction(),
                EUseType.Fishing => new FishingAction(),
                EUseType.Drinking => new DrinkingAction(),
                EUseType.Eating => new EatingAction(),
                _ => throw new ArgumentOutOfRangeException(nameof(useType), useType,
                    "Unknown use type! Need to add new use type")
            };
        }
    }
}
