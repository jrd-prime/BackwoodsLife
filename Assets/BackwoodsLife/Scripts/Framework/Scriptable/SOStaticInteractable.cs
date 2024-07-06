using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "StaticInteractableConfig", menuName = "BLScriptable/Config/StaticInteractableConfig",
        order = 100)]
    public class SOStaticInteractable : ScriptableObject
    {
        public List<SOInteractableMain> StaticInteractables;
    }
}
