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
        public ReactiveProperty<Vector3> Position { get; set; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; set; } = new();

        public void Initialize()
        {
            // Debug.LogWarning("player model init");
        }

        public void Dispose()
        {
            Position.Dispose();
            Rotation.Dispose();
        }
    }
}
