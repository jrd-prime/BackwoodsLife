using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    public class
        UsableAndUpgradeable : InteractableItem<SUseAndUpgradeItem, UseAndUpgradeSystem, EUseAndUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.UseAndUpgrade;

        public override void Process(Action<IInteractZoneState> onInteractionFinished)
        {
            Debug.LogWarning("Use and upgrade process");
        }
    }
}
