using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.ItemTypes
{
    public abstract class UsableI : WorldItem, IUsable
    {
        public abstract SWorldUsableItemConfig usableItemConfig { get; set; }

        public void Use()
        {
            throw new global::System.NotImplementedException();
        }
    }
}
