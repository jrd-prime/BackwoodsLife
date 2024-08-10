using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Common.Structs.Item;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Gameplay.UI;
using BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractZoneState
{
    public class NotEnoughForCollect : IInteractZoneState
    {
        public string StateDesc => "Not enough for collect state";
        private readonly SCollectableItem _notEnoughRequirements;
        private readonly CharacterOverUI _characterOverUIHolder;
        private readonly InteractItemInfoPanelUI _interactItemInfoPanelUI;


        public NotEnoughForCollect(SCollectableItem notEnoughRequirements,
            InteractItemInfoPanelUI interactItemInfoPanelUI)
        {
            Assert.IsNotNull(notEnoughRequirements, "notEnoughRequirements is null");
            Assert.IsNotNull(interactItemInfoPanelUI, "interactItemInfoPanelUI is null");
            // Assert.IsNotNull(characterOverUIHolder, "characterOverUIHolder is null");

            _notEnoughRequirements = notEnoughRequirements;
            // _characterOverUIHolder = characterOverUIHolder;
            _interactItemInfoPanelUI = interactItemInfoPanelUI;
        }

        public void Enter(InteractZone interactZone)
        {
            Debug.LogWarning("Enter Not enough state");
            // _characterOverUIHolder.ShowPopUpForNotEnoughRequirements(_notEnoughRequirements);
            // TODO show not enough
            
            
            _interactItemInfoPanelUI.ShowNotEnoughPanelFor(_notEnoughRequirements);
        }

        public void Exit()
        {
            Debug.LogWarning("Exit Not enough state");
            // _characterOverUIHolder.HideNotEnoughPopUp();
            _interactItemInfoPanelUI.HidePanel();
        }
    }
}
