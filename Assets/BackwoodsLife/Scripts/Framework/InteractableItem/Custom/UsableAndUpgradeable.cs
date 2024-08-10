using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment;

namespace BackwoodsLife.Scripts.Framework.InteractableItem.Custom
{
    public class
        UsableAndUpgradeable : InteractableItem<SUseAndUpgradeItem, UseAndUpgrade, EUseAndUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.UseAndUpgrade;

        public override void Process(Action<IInteractZoneState> onInteractionFinished)
        {
        }
    }
}
