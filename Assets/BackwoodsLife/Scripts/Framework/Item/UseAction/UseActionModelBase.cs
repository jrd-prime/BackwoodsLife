using System;

namespace BackwoodsLife.Scripts.Framework.Item.UseAction
{
    public abstract class UseActionModelBase : IUseActionModel
    {
        public abstract PanelDescriptionData GetDescriptionContent();
    }
}
