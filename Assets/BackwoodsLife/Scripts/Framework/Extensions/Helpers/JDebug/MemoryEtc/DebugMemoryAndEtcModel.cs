using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.Profiling;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Helpers.JDebug.MemoryEtc
{
    public class DebugMemoryAndEtcModel : IStartable
    {
        public ReactiveProperty<int> MonoMemory { get; } = new();
        public ReactiveProperty<int> GraphicsMemory { get; } = new();

        public void Start() => ShowMemoryUsage();


        private async void ShowMemoryUsage()
        {
            while (true)
            {
                int monoUsedMemory = (int)(Profiler.GetMonoUsedSizeLong() / (1024 * 1024));
                int graphicsUsedMemory = (int)(Profiler.GetAllocatedMemoryForGraphicsDriver() / (1024 * 1024));

                MonoMemory.Value = monoUsedMemory;
                GraphicsMemory.Value = graphicsUsedMemory;

                await UniTask.Delay(5000);
            }
        }
    }
}
