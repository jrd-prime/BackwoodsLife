using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public ReadOnlyReactiveProperty<Vector3> PlayerPosition { get; }
        public ReactiveProperty<Vector3> CurrentPosition { get; }
        public ReactiveProperty<string> PlayAnimationByName { get; }
        public ReactiveProperty<bool> DestinationReached { get; }
        public void SetAnimation(string animationName)
        {
            throw new System.NotImplementedException();
        }

        public void MoveToPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void SetObjectForGathering(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public void SetModelPosition(Vector3 transformPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
