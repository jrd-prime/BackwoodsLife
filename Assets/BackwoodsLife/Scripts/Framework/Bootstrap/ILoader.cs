﻿using BackwoodsLife.Scripts.Data.LoadingScreen;
using Cysharp.Threading.Tasks;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    public interface ILoader
    {
        public void AddServiceToInitialize(ILoadingOperation loadingService);
        public UniTask StartServicesInitializationAsync(LoadingScreenViewModel viewModel);
    }
}