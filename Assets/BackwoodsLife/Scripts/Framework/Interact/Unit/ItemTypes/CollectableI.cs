using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable;
using NotImplementedException = System.NotImplementedException;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.ItemTypes
{
    public abstract class CollectableI : WorldItem, ICollectable
    {
        public void Collect(SWorldCollectableItemConfig itemConfig)
        {
            throw new NotImplementedException();
        }
    }
}
