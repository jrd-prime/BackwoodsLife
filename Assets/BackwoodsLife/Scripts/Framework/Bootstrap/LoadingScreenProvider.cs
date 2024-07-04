using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.LoadingScreen;
using UnityEngine;
using VContainer;

// using Game.Scripts.Providers.AssetProvider;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    public interface ILoadingOperation
    {
        public void ServiceInitialization();
        string Description { get; }
    }

    public class LoadingScreenProvider
    {
        // private IAssetProvider _assetProvider;
        private LoadingScreenViewModel _viewModel;
        private LoadingScreenView _view;

        [Inject]
        private void Construct(
            // IAssetProvider assetProvider,
            LoadingScreenViewModel viewModel, LoadingScreenView view)
        {
            // _assetProvider = assetProvider;
            _viewModel = viewModel;
            _view = view;
        }

        public LoadingScreenView View => _view;

        public void LoadAndDestroy(Queue<ILoadingOperation> queue)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                var currentOperation = queue.Dequeue();

                // _viewModel.Header.text = currentOperation.Description;

                Debug.LogWarning(currentOperation.Description);
            }
        }
    }
}
