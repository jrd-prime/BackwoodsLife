using BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public interface IUseActionModel<T> : IUseActionModelBase where T : IPanelData
    {
        public T GetMainPanelData();
    }

    public interface IUseActionModelBase : IUseActionReactive
    {
    }
}
