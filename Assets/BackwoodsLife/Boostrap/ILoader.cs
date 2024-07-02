using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Boostrap;

namespace Game.Scripts.Boostrap
{
    public interface ILoader
    {
        public void AddServiceToInitialize(ILoadingOperation loadingService);
        public UniTask StartServicesInitializationAsync(LoadingScreenViewModel viewModel);
    }
}