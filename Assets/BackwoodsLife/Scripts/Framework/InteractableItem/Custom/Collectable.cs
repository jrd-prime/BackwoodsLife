using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.System.Item;
using BackwoodsLife.Scripts.Gameplay.Environment;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractZoneState;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.InteractableItem.Custom
{
    /// <summary>
    /// Предмет, который находится в игровом мире и который можно собрать. Растения, руда, деревья и т.д.
    /// </summary>
    public abstract class Collectable : InteractableItem<SCollectableItem, Collect, ECollectName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Collect;

        public override void Process(Action<IInteractZoneState> onInteractionFinished)
        {
            Assert.IsNotNull(onInteractionFinished, "interactSystemCallback is null");
            InteractZoneCallback = onInteractionFinished;

            if (!WorldItemConfig.HasCollectables())
            {
                Debug.LogWarning($"{WorldItemConfig.itemName} has no collectables");
                return;
            }

            if (!WorldItemConfig.HasRequirements() || CheckRequirements()) StartCollect();
        }

        private async void StartCollect()
        {
            Debug.LogWarning($"{WorldItemConfig.itemName} Starting collect");
            await PlayerViewModel.SetCollectableActionForAnimationAsync(WorldItemConfig.interactAnimation);
            var processedItems = new List<ItemData>();

            foreach (var item in WorldItemConfig.collectConfig.returnedItems)
            {
                var itemAmount = RandomCollector.GetRandom(item.range.min, item.range.max);

                processedItems.Add(new ItemData { Name = item.item.itemName, Quantity = itemAmount });
            }

            var systemResult = CurrentInteractableSystem.Process(processedItems);

            if (systemResult)
                InteractZoneCallback.Invoke(new SuccessCollected(processedItems, CharacterOverUIHolder));
            else
                Debug.LogError($"{CurrentInteractableSystem.GetType()} failed to process items");
        }

        private bool CheckRequirements()
        {
            Debug.LogWarning($"{WorldItemConfig.itemName} Checking requirements");

            List<ItemDataWithConfig> notEnoughRequirements =
                GameDataManager.CheckRequirementsForCollect(WorldItemConfig.collectConfig.requirementForCollect);

            if (notEnoughRequirements.Count == 0) return true;

            foreach (var requirement in notEnoughRequirements)
            {
                Debug.LogWarning($"Not enough {requirement.item.itemName} {requirement.quantity}");
            }

            InteractZoneCallback.Invoke(new NotEnoughForCollect(WorldItemConfig, InteractItemInfoPanelUI));
            return false;
        }
    }
}
