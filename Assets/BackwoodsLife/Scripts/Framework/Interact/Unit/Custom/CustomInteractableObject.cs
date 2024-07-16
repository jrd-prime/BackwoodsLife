using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
  

    public abstract class CustomInteractableObject<TScriptableType> : InteractableObject
        where TScriptableType : SInWorldInteractable
    {
        public TScriptableType localData => data as TScriptableType;
    }
}
