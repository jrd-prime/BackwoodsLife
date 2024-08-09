using System;
using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items
{
    public abstract class SItemConfig : ScriptableObject
    {
        [Title("Item Config")] [ReadOnly] public string itemName;
        public EWorldItemName worldItemTypeName;
        public string shortDescription;
        public AssetReferenceTexture2D iconReference;

        protected virtual void OnValidate()
        {
            itemName = name;
            if (!iconReference.RuntimeKeyIsValid())
                throw new NullReferenceException($"Icon asset ref is not set. Item config: {name}.");
        }
    }
}
