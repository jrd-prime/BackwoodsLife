using BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Interactable.Config;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.MainConfiguration
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
