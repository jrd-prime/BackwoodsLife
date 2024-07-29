using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.ItemTypes
{
    public abstract class UpgradableI : WorldItem, IUpgradeable
    {
        public SWorldUpgradeableItemConfig upgradeableItemConfig { get; set; }

        public void Upgrade()
        {
            throw new global::System.NotImplementedException();
        }
    }
}
