using R3;
using UnityEngine.Assertions;
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
            Assert.IsNotNull(_model, $"{typeof(ILoadingScreenModel)} is null.");

            // Подписываемся на модель
            _model.LoadingText.Subscribe(x => HeaderView.Value = x);
        }
    }
}
