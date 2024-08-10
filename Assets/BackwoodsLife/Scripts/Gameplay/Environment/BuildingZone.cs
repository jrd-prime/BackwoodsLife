using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Manager.GameData;
using BackwoodsLife.Scripts.Framework.Manager.UIPanel.BuildingPanel;
using BackwoodsLife.Scripts.Framework.System.Building;
using BackwoodsLife.Scripts.Framework.System.Item;
using BackwoodsLife.Scripts.Gameplay.Player;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    public sealed class BuildingZone : MonoBehaviour, IDisposable
    {
        [SerializeField] private SUseAndUpgradeItem itemConfigForBuild;

        private IPlayerViewModel _playerViewModel;
        private Build _build;
        private Spend _spend;
        private BuildingPanelUIController _buildingPanel;
        private readonly CompositeDisposable _disposable = new();

        private bool _isMoving;
        private bool _isInTriggerZone;

        private Action<string, ELevel> _onBuildComplete;
        private GameDataManager _gameDataManager;

        [Inject]
        private void Construct(IPlayerViewModel playerViewModel, Spend spend, Build build,
            BuildingPanelUIController buildingPanelUIController, GameDataManager gameDataManager)
        {
            _playerViewModel = playerViewModel;
            _build = build;
            _spend = spend;
            _buildingPanel = buildingPanelUIController;
            _gameDataManager = gameDataManager;
        }

        private void Awake()
        {
            Assert.IsNotNull(itemConfigForBuild, "ItemConfigForBuild is null!");
            Assert.IsNotNull(_playerViewModel, "PlayerViewModel is null!");
            Assert.IsNotNull(_buildingPanel, "BuildingPanel is null!");
            Assert.IsNotNull(_build, "BuildSystem is null!");
            Assert.IsNotNull(_spend, "SpendSystem is null!");

            _playerViewModel.IsMoving.Subscribe(x => _isMoving = x).AddTo(_disposable);

            _onBuildComplete += OnBuildComplete;
        }

        private void OnBuildComplete(string buildingName, ELevel levelId)
        {
            Debug.LogWarning($"OnBuildComplete: {buildingName} levelId: {levelId}");

            _gameDataManager.buildings.Update(buildingName, (int)levelId);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == (int)JLayers.Player) BuildZoneInteractionStarted();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != (int)JLayers.Player || !_isInTriggerZone) return;
            BuildZoneInteractionFinished();
        }

        private async void BuildZoneInteractionStarted()
        {
            _isInTriggerZone = true;

            /* Wait for the character to stop and only then show the panel.
             Made so that the panel doesn't pop up if the character accidentally hits the build zone trigger*/
            while (_isInTriggerZone && _isMoving) await UniTask.Delay(100);

            if (!_isInTriggerZone || _isMoving) return;

            Debug.LogWarning($"Char in trigger zone and not moving! {name} / {itemConfigForBuild.interactTypes}");

            _buildingPanel.OnBuildButtonClicked1 += BuildButtonClicked;
            _buildingPanel.ShowBuildingPanelFor(itemConfigForBuild);
        }


        private void BuildButtonClicked(List<ItemData> itemsForSpendData)
        {
            Assert.IsNotNull(itemsForSpendData, "ItemsData is null!");

            Destroy(gameObject); // TODO pool or something

            BuildZoneInteractionFinished();

            _spend.Process(itemsForSpendData);
            _build.BuildAsync(itemConfigForBuild, ELevel.Level_1, OnBuildComplete);
        }

        private void BuildZoneInteractionFinished()
        {
            Debug.LogWarning($"Char leave zone or finish interaction! {name} / {itemConfigForBuild.interactTypes}");
            _isInTriggerZone = false;
            _buildingPanel.HideBuildingPanel();
            _buildingPanel.OnBuildButtonClicked1 -= BuildButtonClicked;
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
