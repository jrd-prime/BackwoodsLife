using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Animation.Character
{
    public static class AnimConst
    {
        public static readonly int MoveValue = Animator.StringToHash("MoveValue");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int IsGathering = Animator.StringToHash("IsGathering");
        public static readonly int IsInAction = Animator.StringToHash("IsInAction");
    }
}
