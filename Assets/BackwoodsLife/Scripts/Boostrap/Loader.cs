using System.Collections.Generic;
using BackwoodsLife;
using R3;
using UnityEngine.Assertions;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace Game.Scripts.Boostrap
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
