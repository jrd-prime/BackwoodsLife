using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public interface IConfigManager : ILoadingOperation
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetConfig<T>() where T : ScriptableObject;
        public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfig;
        public AssetReferenceTexture2D GetIconReference(string elementTypeName);
    }
}
