using System;
using BackwoodsLife.Scripts.Framework;
using R3;
using UnityEngine;
using VContainer;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable.Building.Bonfire
{
    public class BonfireViewModel :   IViewModel
    {
        private BonfireModel _bonfireModel;
        public ReactiveProperty<Vector3> Position => _bonfireModel.Position;
        public ReactiveProperty<int> Level => _bonfireModel.Level;

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        [Inject]
        private void Construct(BonfireModel bonfireModel)
        {
            _bonfireModel = bonfireModel;
        }
    }
}
