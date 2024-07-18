using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    public abstract class SItemsConfigList<T> : ScriptableObject
    {
        public Dictionary<string, T> ConfigsCache { get; }
    }
}
