using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Interactable.Config
{
    [CreateAssetMenu(fileName = "NonStaticInteractableObjectsList",
        menuName = "BLScriptable/Configuration/Non Static Objects List",
        order = 2)]
    public class SNonStaticInteractableObjectsList : ScriptableObject
    {
        public List<SInteractableObjectMainConfig> NonStaticInteractables;
    }
}
