using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Scriptable.obj
{
    [CreateAssetMenu(fileName = "name_level_0", menuName = "BLScriptable/Interactable Data/New Level Data",
        order = 100)]
    public class SLevelData : ScriptableObject
    {
        public AssetReferenceGameObject assetReference;

        private void OnValidate()
        {
            Assert.IsNotNull(assetReference, "assetReference must be set " + name);
        }
    }
}
