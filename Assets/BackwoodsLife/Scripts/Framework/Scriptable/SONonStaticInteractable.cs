using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "NonStaticInteractableConfig", menuName = "BLScriptable/Config/NonStaticInteractableConfig",
        order = 100)]
    public class SONonStaticInteractable : ScriptableObject
    {
        public List<SOInteractableMain> NonStaticInteractables;
    }
}
