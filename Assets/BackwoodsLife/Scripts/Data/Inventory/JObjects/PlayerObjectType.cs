using BackwoodsLife.Scripts.Framework.Scriptable;

namespace BackwoodsLife.Scripts.Data.Inventory.JObjects
{
    public enum PlayerObjectType
    {
        None = 0,
        Tool,
        Resource,
        Food
    }

    public abstract class PlayerObject
    {
        public SToolObjectConfig toolObjectConfig { get; set; }
    }
}
