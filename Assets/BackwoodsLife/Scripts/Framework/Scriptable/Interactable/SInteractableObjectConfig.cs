using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Interactable
{
    [CreateAssetMenu(fileName = "Name_level_1", menuName = "BLScriptable/Interactable/Object Config", order = 100)]
    public class SInteractableObjectConfig : ScriptableObject
    {
        [ReadOnly] public string name;
        public AssetReferenceGameObject assetReference;
        public bool upgardable;

        [ShowIf("@this.upgardable == true")] public int upgradeLevel = 1;

        private void OnValidate()
        {
            name = ((Object)this).name;
            Assert.IsNotNull(assetReference, $"Prefab must be set. ScriptableObject: {((Object)this).name}");
        }
    }
}
