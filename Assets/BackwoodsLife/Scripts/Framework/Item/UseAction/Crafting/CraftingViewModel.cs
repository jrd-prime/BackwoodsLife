using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public sealed class CraftingViewModel : CustomUseActionViewModel<CraftingModel>
    {
        public override string Description => "Crafting";

        public override async void Activate(Action onCompleteUseActionCallback)
        {
            Assert.IsNotNull(MainTemplate, $"MainTemplate is null: {this}");
            // interactZoneStateCallback.Invoke();
            // await UniTask.Delay(3000);
            
            
            PanelDescription.Value = Model.GetDescriptionContent();

            PanelToShow.Value = FillPanel();
            // onCompleteUseActionCallback.Invoke();
        }

        public override void Deactivate()
        {
            Debug.LogWarning("Crafting action deactivated");
        }


        protected override TemplateContainer FillPanel()
        {
            var panel = MainTemplate.Instantiate();
            return panel;
        }
    }
}
