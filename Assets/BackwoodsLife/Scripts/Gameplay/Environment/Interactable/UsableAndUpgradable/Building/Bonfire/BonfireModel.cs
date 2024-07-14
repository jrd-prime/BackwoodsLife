using BackwoodsLife.Scripts.Data.Common;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable.Building.Bonfire
{
    public class BonfireModel : IDataModel
    {
        public ReactiveProperty<Vector3> Position { get; private set; } = new();
        public ReactiveProperty<int> Level { get; private set; } = new(0);
        public void Initialize()
        {
        }
    }
}
