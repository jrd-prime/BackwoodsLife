namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingModel : UseActionModelBase
    {
        public override PanelDescriptionData GetDescriptionContent()
        {
            return new PanelDescriptionData { Title = "crafting title", Description = "crafting description" };
        }
    }
}
