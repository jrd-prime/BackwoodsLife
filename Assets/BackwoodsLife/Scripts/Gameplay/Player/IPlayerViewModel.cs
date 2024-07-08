using System;
using BackwoodsLife.Scripts.Framework;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Gameplay.Player
{
    public interface IPlayerViewModel : IViewModel, IDisposable, ITickable
    {
        /// <summary>Позиция из модели</summary>
        public ReadOnlyReactiveProperty<Vector3> PlayerPosition { get; }
        public ReadOnlyReactiveProperty<Quaternion> PlayerRotation { get; }

        public ReactiveProperty<Vector3> CurrentPosition { get; }

        /// <summary>
        /// <c>PlayerView</c> подписан на это св-во и запускает анимацию при изменении значения
        /// </summary>
        public ReactiveProperty<string> PlayAnimationByName { get; }

        /// <summary>
        /// Висит на <c>PlayerViewModel</c>. Устанавливается в <c>PlayerView</c>, когда навмешагент достигает точки.
        /// Подписка в <c>PlayerStateMachine</c> для переключения состояний
        /// </summary>
        public ReactiveProperty<bool> DestinationReached { get; }

        public void SetAnimation(string animationName);
        void MoveToPosition(Vector3 position);
        void SetModelPosition(Vector3 transformPosition);
    }
}
