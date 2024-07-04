using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.LoadingScreen;
using BackwoodsLife.Scripts.Framework.Provider.LoadingScreen;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    /// <summary>
    /// Выступает в качестве инициализатора <see cref="ILoadingOperation"/> сервисов и модели для LoadingScreen
    /// </summary>
    public sealed class Loader : ILoader, ILoadingScreenModel
    {
        private readonly Queue<ILoadingOperation> _loadingQueue = new();

        public ReactiveProperty<string> Header { get; } = new("Default");

        public void AddServiceToInitialize(ILoadingOperation service)
        {
            Assert.IsNotNull(service, "Service is null!");
            _loadingQueue.Enqueue(service);
        }

        public async UniTask StartServicesInitializationAsync(LoadingScreenViewModel viewModel)
        {
            foreach (var service in _loadingQueue)
            {
                // Debug.LogWarning("StartServicesInitializationAsync: " + service.Description);
                Header.Value = service.Description;
                service.ServiceInitialization();
                await UniTask.Delay(100);
            }

            await UniTask.CompletedTask;
        }
    }
}
