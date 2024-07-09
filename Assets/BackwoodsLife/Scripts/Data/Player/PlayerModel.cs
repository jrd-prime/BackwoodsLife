using System;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data.Player
{
    /// <summary>
    /// Player data model
    /// </summary>
    public class PlayerModel : IInitializable, IDisposable, IDataModel
    {
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();

        public void Initialize()
        {
            // Debug.LogWarning("player model init");
        }

        public void Dispose()
        {
            Position.Dispose();
            Rotation.Dispose();
        }

        public void SetPosition(Vector3 position)
        {
            Position.Value = position;
        }

        public void SetRoration(Quaternion rotation)
        {
            Rotation.Value = rotation;
        }
    }
}
