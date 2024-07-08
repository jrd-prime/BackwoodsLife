using BackwoodsLife.Scripts.Framework.Scriptable.Interactable.Config;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Configuration
{
    [CreateAssetMenu(fileName = "MainConfigurations", menuName = "BLScriptable/Configuration/MainConfigurations",
        order = 1)]
    public class SMainConfigurations : ScriptableObject
    {
        [Title("Character")] public SCharacterConfiguration characterConfiguration;
        [Title("Interactable")] public SStaticInteractableObjectsList staticInteractableObjectsList;
        public SNonStaticInteractableObjectsList nonStaticInteractableObjectsList;
    }
}
