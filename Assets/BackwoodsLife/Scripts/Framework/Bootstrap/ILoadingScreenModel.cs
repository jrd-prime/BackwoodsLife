using R3;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    /// <summary>
    /// Интерфейс для модели LoadingScreen
    /// </summary>
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> Header { get; }
    }
}