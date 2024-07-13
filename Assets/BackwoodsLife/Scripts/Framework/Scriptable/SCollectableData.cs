using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name_collect", menuName = "BLScriptable/Interactable Data/New Collectable Data",
        order = 100)]
    public class SCollectableData : ScriptableObject
    {
        public CollectRange collectRange;
    }

    public struct CollectRange
    {
        public int Min;
        public int Max;
    }
}
