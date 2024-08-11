using BackwoodsLife.Scripts.Framework.Item.System.Building;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class BuildingZonesContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<BuildSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
