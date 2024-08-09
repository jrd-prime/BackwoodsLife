using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Records;
using BackwoodsLife.Scripts.Data.Scriptable.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.InteractableItem
{
    public abstract class InteractableItemBase : MonoBehaviour
    {
        public abstract EInteractTypes interactType { get; protected set; }
        public abstract void Process(Action<List<ItemData>> interactSystemCallback);
    }
}
