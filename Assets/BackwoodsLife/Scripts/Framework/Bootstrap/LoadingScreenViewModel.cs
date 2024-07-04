using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    public class LoadingScreenViewModel : IInitializable
    {
        [Inject] private ILoadingScreenModel _model;

        public ReactiveProperty<string> HeaderView { get; } = new();

        public void Initialize()
        {
            Debug.LogWarning("Init view model");
            // Подписываемся на модель
            _model.Header.Subscribe(x => HeaderView.Value = x);
        }
    }
}