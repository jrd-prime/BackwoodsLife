using VContainer;

namespace BackwoodsLife.Scripts.Framework.Item.System.Building
{
    public sealed class UseAndUpgradeSystem : IBuildingSystem
    {
        private UseSystem _useSystem;
        private UpgradeSystem _upgradeSystem;

        [Inject]
        private void Construct(UseSystem useSystem, UpgradeSystem upgradeSystem)
        {
            _useSystem = useSystem;
            _upgradeSystem = upgradeSystem;
        }
        
        
    }
}
