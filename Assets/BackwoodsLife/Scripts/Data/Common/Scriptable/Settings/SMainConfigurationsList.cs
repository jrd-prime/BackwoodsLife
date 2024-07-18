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

        [Title("Items")] public SGameItemsList gameItemsList;
        public SWorldItemsList worldItemsList;
    }
}
