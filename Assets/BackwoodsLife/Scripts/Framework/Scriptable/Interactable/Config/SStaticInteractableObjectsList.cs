using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Interactable.Config
{
    [CreateAssetMenu(fileName = "StaticInteractableObjectsList",
        menuName = "BLScriptable/Configuration/Static Objects List",
        order = 2)]
    public class SStaticInteractableObjectsList : ScriptableObject
    {
        public List<SInteractableObjectMainConfig> staticInteractables;
    }
}
