using BackwoodsLife.Scripts.Gameplay.Player;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Camera
{
    public interface ICameraController
    {
        public UnityEngine.Camera MainCamera { get; }
        void SetFollowTarget(IPlayerViewModel target);
        public void RemoveTarget();
        public void MoveToPosition(Vector3 position);
    }
}
