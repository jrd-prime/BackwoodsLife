using Game.Scripts.Boostrap;
using R3;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.UI.Boostrap
{
    public class LoadingScreenViewModel : IInitializable
    {
        [Inject] private ILoadingScreenModel _model;

        public ReactiveProperty<string> HeaderView { get; } = new();

        public void Initialize()
        {
            // Подписываемся на модель
            _model.Header.Subscribe(x => HeaderView.Value = x);
        }
    }
}