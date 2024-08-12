using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public class CraftingAction : CustomUseAction
    {
        public override string Description => "Crafting";

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}
