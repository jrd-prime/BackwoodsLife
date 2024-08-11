using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    public class Usable : InteractableItem<SUseOnlyItem, UseSystem, EUseName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Use;

        public override void Process(Action<IInteractZoneState> onInteractionFinished)
        {
        }
    }
}
