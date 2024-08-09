using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.System.Building;

namespace BackwoodsLife.Scripts.Framework.InteractableItem
{
    public class Usable : InteractableItem<SUseOnlyItem, Use, EUseName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Use;

        public override void Process(Action<List<ItemData>> interactSystemCallback)
        {
        }
    }
}
