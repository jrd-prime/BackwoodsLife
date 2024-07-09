using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.Input;
using BackwoodsLife.Scripts.Framework.Manager.SaveLoad;
using BackwoodsLife.Scripts.Framework.Scriptable.Configuration;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    public class PlayerViewModel : IPlayerViewModel, IFixedTickable
    {
        public ReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerModel.Position;
        public ReadOnlyReactiveProperty<Quaternion> PlayerRotation => _playerModel.Rotation;

        public ReactiveProperty<Vector3> CurrentPosition { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();
        public ReactiveProperty<bool> DestinationReached { get; } = new(false);

        private CompositeDisposable _disposables = new();
        private PlayerModel _playerModel;
        private IInput _input;
        private Vector3 _direction = Vector3.zero;
        // private PlayerIdle _playerIdle;

        private JoystickModel _joystick;

        private float _moveSpeed;
        private float _rotateSpeedInDeg;
        private IConfigManager _configManager;
        private FollowSystem _followSystem;

        [Inject]
        private void Construct(PlayerModel playerModel, JoystickModel joystickModel, IInput input,
            IConfigManager saveAndLoadManager, FollowSystem followSystem)
        {
            _playerModel = playerModel;
            _input = input;
            _joystick = joystickModel;
            _configManager = saveAndLoadManager;
            _followSystem = followSystem;
        }

        public void Initialize()
        {
            Assert.IsNotNull(_followSystem, $"{_followSystem.GetType()} is null.");
            _followSystem.SetTarget(this);

            var characterConfiguration = _configManager.GetConfig<SCharacterConfiguration>();
            Assert.IsNotNull(characterConfiguration, "Character configuration not found!");

            _moveSpeed = characterConfiguration.moveSpeed;
            _rotateSpeedInDeg = characterConfiguration.rotateSpeedInDeg;

            Debug.LogWarning("INIT VIEWMODEL");
            _joystick.MoveDirection
                // .Skip(1)
                .Subscribe(SetDirection)
                .AddTo(_disposables);
        }


        private void SetDirection(Vector3 direction)
        {
            _direction = direction;
            MoveDirection.Value = direction;

            _playerModel.SetPosition();
            _playerModel.SetRoration();

        }

        public void Dispose() => _disposables.Dispose();

        public void Tick()
        {
            if (_direction != Vector3.zero)
            {
                var deltaTime = Time.deltaTime;

                // Position
                var currentPosition = _playerModel.Position.Value;
                var newPosition = currentPosition + _direction * _moveSpeed * deltaTime;
                _playerModel.Position.Value = newPosition;

                // Rotation
                var currentRotation = _playerModel.Rotation.Value;
                var toRotation = Quaternion.LookRotation(_direction);
                var newRotation =
                    Quaternion.RotateTowards(currentRotation, toRotation, _rotateSpeedInDeg * deltaTime);
                _playerModel.Rotation.Value = newRotation;
            }
        }


        public void SetAnimation(string animationName) => PlayAnimationByName.Value = animationName;
        public void MoveToPosition(Vector3 position) => _playerModel.Position.Value = position;
        public void SetModelPosition(Vector3 transformPosition) => CurrentPosition.Value = transformPosition;

        public void FixedTick()
        {
            Debug.LogWarning("Fixed tick");
        }
    }
}
