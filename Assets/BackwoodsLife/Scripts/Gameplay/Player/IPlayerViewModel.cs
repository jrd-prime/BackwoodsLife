using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework;
using Cysharp.Threading.Tasks;
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

        public ReactiveProperty<CharacterAction> CharacterAction { get; }
        public ReactiveProperty<string> CancelCharacterAction { get; }
        public ReactiveProperty<bool> IsInAction { get; }

        public UniTask SetCollectableActionForAnimationAsync(EInteractAnimation interactAnimation);
        public void SetModelPosition(Vector3 rbPosition);
        public void SetModelRotation(Quaternion rbRotation);
    }
}
