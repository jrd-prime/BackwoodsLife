using BackwoodsLife.Scripts.Data.Player;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.UI.CharacterOverUI
{
    public class CharacterOverUI : UIView
    {
        private PlayerModel _playerModel;
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void Awake()
        {
            Assert.IsNotNull(_playerModel, "_playerModel is null");
            _playerModel.Position.Subscribe(pos => { transform.position = pos; }).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}
