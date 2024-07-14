using BackwoodsLife.Scripts.Data.Common.Scriptables;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
  

    public abstract class CustomInteractableObject<TScriptableType> : InteractableObject
        where TScriptableType : SInteractableObject
    {
        public TScriptableType localData => data as TScriptableType;
    }
}
