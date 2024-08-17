using R3;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public interface IUseActionReactive
    {
        public ReactiveProperty<PanelDescriptionData> DescriptionPanelData { get; }
    }
}
