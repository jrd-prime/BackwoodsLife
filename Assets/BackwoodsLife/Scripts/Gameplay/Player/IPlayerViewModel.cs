using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
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

        /// <summary>MoveDirection from Joystick</summary>
        public ReadOnlyReactiveProperty<Vector3> MoveDirection { get; }

        public ReadOnlyReactiveProperty<float> MoveSpeed { get; }
        public ReadOnlyReactiveProperty<float> RotationSpeed { get; }


        public Subject<Unit> IsGathering { get; }
        
        /// <summary>
        /// <c>PlayerView</c> подписан на это св-во и запускает анимацию при изменении значения
        /// </summary>
        public ReactiveProperty<string> PlayAnimationByName { get; }

        public void SetCollectableAction(EInteractType interactType);
        public void SetAnimation(string animationName);
        public void SetModelPosition(Vector3 rbPosition);
        public void SetModelRotation(Quaternion rbRotation);
    }
}
