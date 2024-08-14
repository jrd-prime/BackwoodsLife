using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingViewModel : CustomUseActionViewModel<CraftingModel>
    {
        public override string Description => "Crafting";

        public override async void Activate(Action onCompleteUseActionCallback)
        {
            // interactZoneStateCallback.Invoke();
            // await UniTask.Delay(3000);

            PanelToShow.Value = FillPanel();
            // onCompleteUseActionCallback.Invoke();
        }

        public override void Deactivate()
        {
            Debug.LogWarning("Crafting action deactivated");
        }

        protected override TemplateContainer FillPanel()
        {
            return new TemplateContainer();
        }
    }
}
