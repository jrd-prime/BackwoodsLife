using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    /// <summary>
    /// Описание объекта, который можно хранить в инвентаре
    /// </summary>
    public abstract class SCommonData : ScriptableObject
    {
        [ReadOnly] public string nameId;
        public AssetReferenceTexture2D icon;

        [Title("Settings")] [Range(1, 100)] public int maxStackSize = 20;
        public bool canBeSold = true;

        [ShowIf("@canBeSold == true")] [Range(1, 1000)]
        public float sellPrice = 1;

        private void OnValidate()
        {
            nameId = name;
        }
    }
}
