using System;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Item.System.Building;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using BackwoodsLife.Scripts.Gameplay.UI.Panel.UseActionsPanel;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    public class UsableAndUpgradeable : InteractableItem<SUseAndUpgradeItem, UseAndUpgradeSystem, EUseAndUpgradeName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.UseAndUpgrade;

        public override void Process(Action<IInteractZoneState> interactZoneCallback)
        {
            UseActionsPanelUIController useActionsPanelUIController = Container.Resolve<UseActionsPanelUIController>();
            Assert.IsNotNull(useActionsPanelUIController, "UseActionsPanelUIController is null");

            Debug.LogWarning("Use and upgrade process");

            foreach (var useAction in WorldItemConfig.useConfig.useActions)
            {
                Debug.LogWarning($"Use action: {useAction.useType}");
            }


            var useActionsState = new UseActionsState(WorldItemConfig.useConfig.useActions, interactZoneCallback);

            Container.Inject(useActionsState);

            interactZoneCallback.Invoke(useActionsState);
        }
    }
}
