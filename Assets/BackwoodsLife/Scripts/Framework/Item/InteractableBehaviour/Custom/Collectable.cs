﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Extensions;
using BackwoodsLife.Scripts.Framework.Extensions.Helpers;
using BackwoodsLife.Scripts.Framework.Item.System.Item;
using BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Item.InteractableBehaviour.Custom
{
    /// <summary>
    /// Предмет, который находится в игровом мире и который можно собрать. Растения, руда, деревья и т.д.
    /// </summary>
    public abstract class Collectable : InteractableItem<SCollectableItem, CollectSystem, ECollectName>
    {
        public override EInteractTypes interactType { get; protected set; } = EInteractTypes.Collect;

        public override void Process(Action<IInteractZoneState> interactZoneCallback)
        {
            Assert.IsNotNull(interactZoneCallback, "interactSystemCallback is null");
            InteractZoneCallback = interactZoneCallback;

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

            var notEnoughRequirements =
                GameDataManager.CheckRequirementsForCollect(WorldItemConfig.collectConfig.requirementForCollect);

            if (GameDataManager.IsEnoughForCollect(WorldItemConfig.collectConfig.requirementForCollect)) return true;

            InteractZoneCallback.Invoke(new NotEnoughForCollect(WorldItemConfig, InteractItemInfoPanelUI));
            return false;
        }
    }
}
