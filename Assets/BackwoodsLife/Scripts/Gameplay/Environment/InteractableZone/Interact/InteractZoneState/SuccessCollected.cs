using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    public class SuccessCollected : IInteractZoneState
    {
        public string StateDesc => "Success collected state";
        
        private readonly List<ItemData> _processedItems;
        private readonly CharacterOverUI _characterOverUIHolder;

        public SuccessCollected(List<ItemData> processedItems, CharacterOverUI characterOverUIHolder)
        {
            Assert.IsNotNull(processedItems, "processedItems is null");
            Assert.IsNotNull(characterOverUIHolder, "characterOverUIHolder is null");

            _processedItems = processedItems;
            _characterOverUIHolder = characterOverUIHolder;
        }


        public void Enter(InteractZone interactZone)
        {
            Debug.LogWarning($"{StateDesc}");
            _characterOverUIHolder.ShowPopUpFor(_processedItems);
            interactZone.DestroyO();
        }

        public void Exit()
        {
            Debug.LogWarning("Exit Success collected state");
        }
    }
}
