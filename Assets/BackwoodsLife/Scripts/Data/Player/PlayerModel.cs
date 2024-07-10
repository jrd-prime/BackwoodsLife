using System;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Player
{
    /// <summary>
    /// Player data model
    /// </summary>
    public class PlayerModel : IDisposable, IDataModel
    {
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();

        public void SetPosition(Vector3 position) => Position.Value = position;
        public void SetRotation(Quaternion rotation) => Rotation.Value = rotation;
        public void SetMoveDirection(Vector3 moveDirection) => MoveDirection.Value = moveDirection;

        public void Dispose()
        {
            Position.Dispose();
            Rotation.Dispose();
            MoveDirection.Dispose();
        }
    }
}
