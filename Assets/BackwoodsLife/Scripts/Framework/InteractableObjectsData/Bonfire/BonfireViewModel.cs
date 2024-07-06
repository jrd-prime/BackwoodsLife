using System;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Data.InteractableObjectsData.Bonfire
{
    public class BonfireViewModel : IInitializable
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
