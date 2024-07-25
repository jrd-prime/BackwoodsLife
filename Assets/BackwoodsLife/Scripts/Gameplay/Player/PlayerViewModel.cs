using System;
using BackwoodsLife.Scripts.Data.Animation.Character;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Settings;
using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Framework.Manager.Camera;
using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using BackwoodsLife.Scripts.Gameplay.UI.Joystick;
using Cysharp.Threading.Tasks;
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
        public ReactiveProperty<CharacterAction> CharacterAction { get; } = new();
        public ReactiveProperty<string> CancelCharacterAction { get; } = new();
        public ReactiveProperty<bool> IsInAction { get; } = new(false);


        private IConfigManager _configManager;
        private PlayerModel _model;
        private FollowSystem _followSystem;
        private JoystickModel _joystick;
        private readonly CompositeDisposable _disposables = new();
        private readonly CharacterAction _characterActionReset = new() { AnimationParamId = 0, Value = false };

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
            //TODO on drop joystick slow speed down
            _joystick.MoveDirection
                .Subscribe(joystickDirection => { _model.SetMoveDirection(joystickDirection); })
                .AddTo(_disposables);
        }

        public void SetModelPosition(Vector3 rbPosition) => _model.SetPosition(rbPosition);
        public void SetModelRotation(Quaternion rbRotation) => _model.SetRotation(rbRotation);

        public void Dispose() => _disposables.Dispose();

        public async void SetCollectableActionForAnimationAsync(EInteractType interactType)
        {
            Debug.LogWarning("SetCollectableActionForAnimation called");
            IsInAction.Value = true;

            switch (interactType)
            {
                case EInteractType.Gathering:
                    await NewActionAsync(AnimConst.IsGathering, 5000);
                    break;
                case EInteractType.Mining:
                    // IsMining.Value = true;
                    break;
                case EInteractType.Fishing:
                    // IsFishing.Value = true;
                    break;
                case EInteractType.Hunting:
                    // IsHunting.Value = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null);
            }

            IsInAction.Value = false;
        }

        private async UniTask NewActionAsync(int animParameterId, int actionDelay)
        {
            CharacterAction.Value = new CharacterAction { AnimationParamId = animParameterId, Value = true };
            await UniTask.Delay(actionDelay);
            CharacterAction.Value = new CharacterAction { AnimationParamId = animParameterId, Value = false };

            // reset // TODO bad
            CharacterAction.Value = _characterActionReset;
        }
    }

    public struct CharacterAction
    {
        public int AnimationParamId;
        public bool Value;
    }
}
