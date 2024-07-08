using R3;

namespace BackwoodsLife.Scripts.Framework.Bootstrap
{
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> LoadingText { get; }
    }
}
