using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Manager.Input;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerModel.Position;

        public ReactiveProperty<Vector3> CurrentPosition { get; } = new();
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();
        public ReactiveProperty<bool> DestinationReached { get; } = new(false);

        private CompositeDisposable _disposables = new();
        private PlayerModel _playerModel;
        private IInput _input;
        // private PlayerIdle _playerIdle;

        private GameObject _objectForGathering = null;

        [Inject]
        private void Construct(PlayerModel playerModel, IInput input)
        {
            _playerModel = playerModel;
            _input = input;
        }

        public void Initialize()
        {
            Debug.LogWarning("INIT VIEWMODEL");
        }

        public void Dispose() => _disposables.Dispose();

        public void Tick()
        {
            Debug.LogWarning("TICK");
        }

        public void SetAnimation(string animationName) => PlayAnimationByName.Value = animationName;
        public void MoveToPosition(Vector3 position) => _playerModel.Position.Value = position;
        public void SetObjectForGathering(GameObject obj) => _objectForGathering = obj;
        public void SetModelPosition(Vector3 transformPosition) => CurrentPosition.Value = transformPosition;
    }
}
