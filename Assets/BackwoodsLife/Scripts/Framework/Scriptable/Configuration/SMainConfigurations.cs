using BackwoodsLife.Scripts.Framework.Scriptable.Interactable.Config;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Configuration
{
    [CreateAssetMenu(fileName = "MainConfigurations", menuName = "BLScriptable/Configuration/MainConfigurations",
        order = 1)]
    public class SMainConfigurations : ScriptableObject
    {
        public SStaticInteractableObjectsList staticInteractableObjectsList;
        public SNonStaticInteractableObjectsList nonStaticInteractableObjectsList;
    }
}
