using BackwoodsLife.Scripts.Framework.System.Building;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class BuildingZonesContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<Build>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
