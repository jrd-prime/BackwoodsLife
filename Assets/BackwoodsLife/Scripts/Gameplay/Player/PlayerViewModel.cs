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
        
        public Rigidbody Rigidbody { get; set; }
        public Animator Animator { get; set; }

        private CompositeDisposable _disposables = new();
        private PlayerModel _playerModel;
        private IInput _input;
        private Vector3 previousDirection = Vector3.zero;
        private Vector3 _direction = Vector3.zero;
        // private PlayerIdle _playerIdle;

        private JoystickModel _joystick;

        public Vector3 moveDirection { get; private set; }
        private float _moveSpeed;
        private float _rotateSpeedInDeg;
        private IConfigManager _configManager;
        private FollowSystem _followSystem;

        public bool recievedNewDirection { get; set; }


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

            _joystick.MoveDirection
                // .Skip(1)
                .Subscribe(SetDirection)
                .AddTo(_disposables);
        }

        public void FixedTick()
        {
            if (recievedNewDirection)
            {
                Debug.LogWarning("Fixed tick");

                var newPosition = PlayerPosition.CurrentValue + moveDirection * (5f * Time.fixedDeltaTime);
                
                _playerModel.SetPosition();
                _playerModel.SetRoration();
                PlayerPosition += ;

                recievedNewDirection = false;
            }
            
            
            _rigidbody.position += moveDirection * (5f * Time.fixedDeltaTime);
            if (moveDirection.sqrMagnitude > 0)
            {
                // We create a Quaternion, the type of variable we use to represent rotations and
                // we use Quaternion.LookRotation to look at our moveInput vector which always points
                // towards the moving direction, and we say that we want to rotate the Vector3.up (Y axis).
                Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                // Then we pass that rotation to our Rigidbody rot using Quaternion.Lerp which is a method
                // to interpolate between two quaternions by a given time. In our case we use as the first
                // Quaternion the _Rigidbody.rotation and as a second Quaternion our previously calculated rotation,
                // then we add time by writing Time.fixedDeltaTime (fixed cuz we are in the method FixedUpdate)
                // and we multiply that by a rotationRate to make it go faster or slower.
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, rotation, Time.fixedDeltaTime * 100);
            }
        }

        private void SetDirection(Vector3 direction)
        {
            recievedNewDirection = true;
            moveDirection = direction;
            _direction = direction;
            MoveDirection.Value = direction;
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
    }
}
