using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.InteractableObjectsData.Bonfire
{
    public class BonfireModel
    {
        public ReactiveProperty<Vector3> Position { get; private set; } = new();
        public ReactiveProperty<int> Level { get; private set; } = new(0);
    }
}
