using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.GameItem;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public abstract class CustomWorldInteractableItem<TScriptableType> : WorldInteractableItem
        where TScriptableType : SWorldItemConfigNew
    {
        protected TScriptableType config;
    }
}
