using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public class DrinkingAction : CustomUseAction
    {
        public override string Description => "Cooking";

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
