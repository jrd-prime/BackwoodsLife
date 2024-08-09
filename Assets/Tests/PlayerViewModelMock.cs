using BackwoodsLife.Scripts.Data;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Gameplay.Player;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Tests
{
    public class PlayerViewModelMock : IPlayerViewModel
    {
        private readonly PlayerModel _model;

        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _model.MoveDirection;
        public Subject<Unit> IsGathering { get; }
        public ReactiveProperty<string> CancelCharacterAction { get; }
        public ReactiveProperty<bool> IsInAction { get; }
        public ReadOnlyReactiveProperty<bool> IsMoving { get; }
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();
        public ReadOnlyReactiveProperty<float> MoveSpeed => _model.MoveSpeed;
        public ReadOnlyReactiveProperty<float> RotationSpeed => _model.RotationSpeed;
        public ReactiveProperty<CharacterAction> CharacterAction { get; }

        public void Initialize()
        {
        }

        public PlayerViewModelMock(PlayerModel model, float moveSpeed, float rotationSpeed)
        {
            _model = model;
            _model.SetMoveSpeed(moveSpeed);
            _model.SetRotationSpeed(rotationSpeed);
        }

        public async UniTask SetCollectableActionForAnimationAsync(EInteractAnimation interactAnimation)
        {
            throw new System.NotImplementedException();
        }

        public void SetAnimation(string animationName)
        {
            PlayAnimationByName.Value = animationName;
        }

        public void SetModelPosition(Vector3 rbPosition)
        {
            _model.SetPosition(rbPosition);
        }

        public void SetModelRotation(Quaternion rbRotation)
        {
            _model.SetRotation(rbRotation);
        }

        public void Dispose()
        {
            Position.Dispose();
            Rotation.Dispose();
            MoveDirection.Dispose();
            PlayAnimationByName.Dispose();
            _model.Dispose();
        }
    }
}
