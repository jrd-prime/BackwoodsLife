using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    /// <summary>
    /// Обработка интерактабл объекта, который можно использовать и улучшать.
    /// Вызывет состояние "Use actions state" у триггер зоны
    /// </summary>
    public class UsableAndUpgradeable : InteractableItem<SUseAndUpgradeItem, UseAndUpgradeSystem, EUseAndUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.UseAndUpgrade;

        public override void Process(Action<IInteractZoneState> interactZoneCallback)
        {
            foreach (var useAction in WorldItemConfig.useConfig.useActions)
                Debug.LogWarning($"Use action: {useAction.useType}");

            var useActionsState = new UseActionsState(WorldItemConfig, interactZoneCallback);

            Container.Inject(useActionsState);

            interactZoneCallback.Invoke(useActionsState);
        }
    }
}
