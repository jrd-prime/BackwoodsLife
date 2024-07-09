using System;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Framework.Manager.Input;
using BackwoodsLife.Scripts.Framework.Scriptable.Configuration;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        // Reactive properties
        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;

        // Private
        private IInput _input;
        private PlayerModel _model;
        private FollowSystem _followSystem;
        private JoystickModel _joystick;
        private CompositeDisposable _disposables = new();


        public ReactiveProperty<Vector3> CurrentPosition { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();
        public ReactiveProperty<bool> DestinationReached { get; } = new(false);

        private Vector3 previousDirection = Vector3.zero;
        private Vector3 _direction = Vector3.zero;
        // private PlayerIdle _playerIdle;


        public Vector3 moveDirection { get; private set; }
        private float _moveSpeed;
        private float _rotateSpeedInDeg;
        private IConfigManager _configManager;

        public bool recievedNewDirection { get; set; }

        [Inject]
        private void Construct(PlayerModel playerModel, JoystickModel joystickModel, IInput input,
            IConfigManager saveAndLoadManager, FollowSystem followSystem)
        {
            _model = playerModel;
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

            // _joystick.MoveDirection
            //     // .Skip(1)
            //     .Where(x => x != Vector3.zero)
            //     .Subscribe(direction =>
            //     {
            //         _direction = direction;
            //         MoveDirection.Value = direction;
            //     })
            //     .AddTo(_disposables);

            var fixdt = Time.fixedDeltaTime;
            Debug.Log(fixdt);

            Observable.Interval(TimeSpan.FromSeconds(fixdt))
                .Subscribe(_ =>
                {
                    _direction = _joystick.MoveDirection.Value;
                    MoveDirection.Value = _joystick.MoveDirection.Value;
                    // Тут можно обновлять ваше состояние или выполнять другие действия
                })
                .AddTo(_disposables);
        }

        public void FixedTick()
        {
            // if (_direction != Vector3.zero)
            // {
            var position = _direction * 1f;

            _model.SetPosition(position);

            if (_direction.sqrMagnitude > 0)
            {
                var rotation = Quaternion.LookRotation(_direction, Vector3.up);


                _model.SetRoration(Quaternion.Lerp(Rotation.CurrentValue, rotation,
                    // Time.fixedDeltaTime * 
                    100));
            }
            // }
        }

        public void Dispose() => _disposables.Dispose();
        public void SetAnimation(string animationName) => PlayAnimationByName.Value = animationName;
        public void MoveToPosition(Vector3 position) => _model.Position.Value = position;
        public void SetModelPosition(Vector3 transformPosition) => CurrentPosition.Value = transformPosition;
    }
}
