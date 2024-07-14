using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE.Configuration
{
    [CreateAssetMenu(fileName = "CharacterConfiguration",
        menuName = "BLScriptable/Configuration/Character Configuration",
        order = 1)]
    public class SCharacterConfiguration : ScriptableObject
    {
        [Range(0.1f, 100f)] public float moveSpeed = 5f;
        [FormerlySerializedAs("rotateSpeedInDeg")] [Range(45f, 270f)] public float rotationSpeed = 180f;
    }
}
