using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public class RestingAction : CustomUseAction
    {
        public override string Description => "Resting";

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
