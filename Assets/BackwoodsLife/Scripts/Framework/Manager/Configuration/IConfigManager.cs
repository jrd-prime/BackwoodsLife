using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public interface IConfigManager : ILoadingOperation, IInitializable
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetConfig<T>() where T : ScriptableObject;
        public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfig;
    }
}
