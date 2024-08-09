using VContainer;

namespace BackwoodsLife.Scripts.Framework.System.Building
{
    public class UseAndUpgrade : IBuildingSystem
    {
        private Use _use;
        private Upgrade _upgrade;

        [Inject]
        private void Construct(Use use, Upgrade upgrade)
        {
            _use = use;
            _upgrade = upgrade;
        }
    }
}
