using System;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public interface IUseActionViewModel
    {
        public string Description { get; }
        public void Activate(Action onCompleteUseActionCallback);
        public void Deactivate();
    }
}
