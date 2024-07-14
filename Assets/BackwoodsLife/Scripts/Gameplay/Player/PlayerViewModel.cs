using BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Configuration;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _model.MoveDirection;
        public ReadOnlyReactiveProperty<float> MoveSpeed => _model.MoveSpeed;
        public ReadOnlyReactiveProperty<float> RotationSpeed => _model.RotationSpeed;
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();

        private IConfigManager _configManager;
        private PlayerModel _model;
        private FollowSystem _followSystem;
        private JoystickModel _joystick;
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(PlayerModel playerModel, JoystickModel joystickModel, IConfigManager saveAndLoadManager,
            FollowSystem followSystem)
        {
            _model = playerModel;
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

            _model.SetMoveSpeed(characterConfiguration.moveSpeed);
            _model.SetRotationSpeed(characterConfiguration.rotationSpeed);

            Subscribe();
        }

        private void Subscribe()
        {
            _joystick.MoveDirection
                .Subscribe(joystickDirection => { _model.SetMoveDirection(joystickDirection); })
                .AddTo(_disposables);
        }

        public void SetAnimation(string animationName) => PlayAnimationByName.Value = animationName;
        public void SetModelPosition(Vector3 rbPosition) => _model.SetPosition(rbPosition);
        public void SetModelRotation(Quaternion rbRotation) => _model.SetRotation(rbRotation);

        public void Dispose() => _disposables.Dispose();
    }
}
