using BackwoodsLife.Scripts.Data.Const;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Scriptable.Settings
{
    [CreateAssetMenu(
        fileName = "CharacterConfiguration", 
        menuName = SOPathName.ConfigPath + "Character Configuration",
        order = 100)]
    public class SCharacterConfig : ScriptableObject
    {
        [Range(0.1f, 100f)] public float moveSpeed = 5f;

        [FormerlySerializedAs("rotateSpeedInDeg")] [Range(45f, 270f)]
        public float rotationSpeed = 180f;
    }
}
