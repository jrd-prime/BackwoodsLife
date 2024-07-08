using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    /// <summary>
    /// Serves as the initializer for <see cref="ILoadingOperation"/> services and the model for the LoadingScreen
    /// </summary>
    public sealed class Loader : ILoader, ILoadingScreenModel
    {
        private readonly Queue<ILoadingOperation> _loadingQueue = new();

        public ReactiveProperty<string> LoadingText { get; } = new("Default");

        public void AddServiceToInitialize(ILoadingOperation service)
        {
            Assert.IsNotNull(service, "Service is null!");
            _loadingQueue.Enqueue(service);
        }

        public async UniTask StartServicesInitializationAsync()
        {
            var count = 0; //TODO remove

            foreach (var service in _loadingQueue)
            {
                try
                {
                    Debug.LogWarning(
                        $"\t<color=cyan>{count++}/{_loadingQueue.Count} / Init Service: {service.GetType().Name}</color>"); //TODO remove
                    LoadingText.Value = $"Loading: {service.Description}..";
                    service.ServiceInitialization();
                    await UniTask.Delay(100);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to initialize {service.GetType().Name}: {ex.Message}");
                }
            }

            Debug.LogWarning($"\t<color=cyan>Services initialized.</color>"); //TODO remove
            await UniTask.CompletedTask;
        }
    }
}
