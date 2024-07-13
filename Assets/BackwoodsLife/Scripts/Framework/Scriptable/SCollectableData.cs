using System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name_collect", menuName = "BLScriptable/Interactable Data/New Collectable Data",
        order = 100)]
    public class SCollectableData : ScriptableObject
    {
        public CollectRange collectRange;
    }

    [Serializable]
    public struct CollectRange
    {
        public int min;
        public int max;
    }
}
