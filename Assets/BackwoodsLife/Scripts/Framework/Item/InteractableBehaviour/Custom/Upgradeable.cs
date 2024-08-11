using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    public class Upgradeable : InteractableItem<SUpgradeOnlyItem, UpgradeSystem, EUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Upgrade;

        public override void Process(Action<IInteractZoneState> onInteractionFinished)
        {
        }
    }
}
