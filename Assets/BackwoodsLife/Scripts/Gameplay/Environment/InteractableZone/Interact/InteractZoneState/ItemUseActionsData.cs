using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    public struct ItemUseActionsData
    {
        public Sprite Icon;
        public string Title;
        public string Description;
        public List<UseAction> UseActions;
    }
}
