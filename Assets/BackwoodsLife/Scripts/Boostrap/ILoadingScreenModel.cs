using R3;

namespace Game.Scripts.Boostrap
{
    /// <summary>
    /// Интерфейс для модели LoadingScreen
    /// </summary>
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> Header { get; }
    }
}