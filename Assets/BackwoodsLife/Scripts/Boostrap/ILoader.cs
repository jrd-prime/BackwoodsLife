using BackwoodsLife;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.Boostrap
{
    public interface ILoader
    {
        public void AddServiceToInitialize(ILoadingOperation loadingService);
        public UniTask StartServicesInitializationAsync(LoadingScreenViewModel viewModel);
    }
}