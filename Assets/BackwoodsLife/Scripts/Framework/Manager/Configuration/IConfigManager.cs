using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public interface IConfigManager: ILoadingOperation
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetConfig<T>() where T : ScriptableObject;
        public T GetScriptableConfig<T>() where T : IConfigScriptable;
    }
}
