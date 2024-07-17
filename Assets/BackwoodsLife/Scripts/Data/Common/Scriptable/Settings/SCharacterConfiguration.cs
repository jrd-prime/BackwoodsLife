using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Settings
{
    [CreateAssetMenu(
        fileName = "CharacterConfiguration", 
        menuName = SOPathName.ConfigPath + "Character Configuration",
        order = 100)]
    public class SCharacterConfiguration : ScriptableObject
    {
        [Range(0.1f, 100f)] public float moveSpeed = 5f;

        [FormerlySerializedAs("rotateSpeedInDeg")] [Range(45f, 270f)]
        public float rotationSpeed = 180f;
    }
}
