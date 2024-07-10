using BackwoodsLife.Scripts.Data.Player;
using BackwoodsLife.Scripts.Gameplay.Player;
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
        public ReactiveProperty<string> PlayAnimationByName { get; } = new();
        public float MoveSpeed { get; }
        public float RotationSpeed { get; }

        public void Initialize()
        {
        }

        public PlayerViewModelMock(PlayerModel model, float moveSpeed, float rotationSpeed)
        {
            _model = model;
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
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
