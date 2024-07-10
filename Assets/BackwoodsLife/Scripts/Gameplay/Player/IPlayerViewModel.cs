using System;
using BackwoodsLife.Scripts.Framework;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    public interface IPlayerViewModel : IViewModel, IDisposable
    {
        /// <summary>Position from model</summary>
        public ReadOnlyReactiveProperty<Vector3> Position { get; }

        /// <summary>Rotation from model</summary>
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; }

        public ReadOnlyReactiveProperty<Vector3> MoveDirection { get; }

        /// <summary>
        /// <c>PlayerView</c> подписан на это св-во и запускает анимацию при изменении значения
        /// </summary>
        public ReactiveProperty<string> PlayAnimationByName { get; }

        public float MoveSpeed { get; }
        public float RotationSpeed { get; }


        public void SetAnimation(string animationName);
        public void SetModelPosition(Vector3 rbPosition);
        public void SetModelRotation(Quaternion rbRotation);
    }
}
