using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction.Crafting
{
    public class CraftingViewModel : CustomUseActionViewModel<CraftingModel>
    {
        public override string Description => "Crafting";

        public async override void Activate(Action onCompleteUseActionCallback)
        {
            Show.Value = 2;
            // interactZoneStateCallback.Invoke();
            await UniTask.Delay(3000);

            onCompleteUseActionCallback.Invoke();
        }

        public override void Deactivate()
        {
            Debug.LogWarning("Crafting action deactivated");
        }
    }
}
