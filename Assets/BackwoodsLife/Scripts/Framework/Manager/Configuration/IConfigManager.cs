﻿using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Framework.Bootstrap;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace BackwoodsLife.Scripts.Framework.Manager.Configuration
{
    public interface IConfigManager : ILoadingOperation
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetItemConfig<T>(string elementTypeName) where T : SItemConfig;
        public T GetWorldItemConfig<T>(string enumTypeName) where T : SWorldItemConfigNew;
        public AssetReferenceTexture2D GetIconReference(string elementTypeName);
        public T GetConfig<T>() where T : ScriptableObject;
    }
}
