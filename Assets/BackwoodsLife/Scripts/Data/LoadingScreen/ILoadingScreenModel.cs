using R3;

namespace BackwoodsLife.Scripts.Data.LoadingScreen
{
    /// <summary>
    /// Интерфейс для модели LoadingScreen
    /// </summary>
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> Header { get; }
    }
}