using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.newnew;
using BackwoodsLife.Scripts.Framework.Interact.Unit.ItemTypes;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable
{
    public class Bonfire : WorldItemNew
    {
    }

    public abstract class WorldItemNew : MonoBehaviour
    {
        [SerializeField] protected SWorldItemConfigNew worldItemConfig;
    }


    public interface IUpgradeable
    {
        public virtual void Upgrade(SWorldUpgradeableItemConfig upgradeableItemConfig)
        {
            Debug.LogWarning("UpgradableI.Upgrade() not implemented");
        }
    }

    public interface IUsable
    {
        public virtual void Use(SWorldUsableItemConfig usableItemConfig)
        {
            Debug.LogWarning("UsableI.Use() not implemented");
        }
    }

    public interface ICollectable
    {
        public void Collect(SWorldCollectableItemConfig itemConfig);
    }
}
