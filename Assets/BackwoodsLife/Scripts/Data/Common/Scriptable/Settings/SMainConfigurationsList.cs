using BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Interactable.Config;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(
        fileName = "MainConfigurations",
        menuName = SOPathName.ConfigPath + "Main Configurations List",
        order = 100)]
    public class SMainConfigurationsList : ScriptableObject
    {
        [Title("Character")] public SCharacterConfiguration characterConfiguration;
        [Title("Interactable")] public SStaticInteractableObjectsList staticInteractableObjectsList;
        public SNonStaticInteractableObjectsList nonStaticInteractableObjectsList;


        [Title("Items")] public SGameItemsList gameItemsList;
        public SWorldItemsList worldItemsList;
    }
}
