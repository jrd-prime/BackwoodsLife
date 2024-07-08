using BackwoodsLife.Scripts.Data;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.InteractableObjects.Bonfire
{
    public class BonfireModel : IDataModel
    {
        public ReactiveProperty<Vector3> Position { get; private set; } = new();
        public ReactiveProperty<int> Level { get; private set; } = new(0);
    }
}
