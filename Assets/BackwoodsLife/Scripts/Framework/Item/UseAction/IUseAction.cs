namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public interface IUseAction
    {
        public string Description { get; }
        public void Activate();
        public void Deactivate();
    }
}
