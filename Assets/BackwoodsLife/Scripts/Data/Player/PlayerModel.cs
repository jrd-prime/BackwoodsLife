using System;
using Game.Scripts.Managers.SaveLoad;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data.Player
{
    /// <summary>
    /// Модель данных игрока
    /// </summary>
    public class PlayerModel : IData, IInitializable, IDisposable
    {
        public ReactiveProperty<Vector3> Position { get; set; } = new();
        public ReactiveProperty<Vector3> Rotation { get; set; } = new();

        public void SetPosition(Vector3 vector3)
        {
            Debug.LogWarning("<color=green>Model.SetPosition " + vector3 + "</color>");
            Position.Value = vector3;
        }

        public void Initialize()
        {
            Debug.LogWarning("player model init");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
