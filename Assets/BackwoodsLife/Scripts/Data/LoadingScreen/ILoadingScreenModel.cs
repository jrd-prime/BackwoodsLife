using R3;

namespace BackwoodsLife.Scripts.Data.LoadingScreen
{
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> LoadingText { get; }
    }
}
