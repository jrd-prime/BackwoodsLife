using BackwoodsLife.Scripts.Framework.Manager.Configuration;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable.Configuration
{
    [CreateAssetMenu(fileName = "CharacterConfiguration",
        menuName = "BLScriptable/Configuration/Character Configuration",
        order = 1)]
    public class SCharacterConfiguration : ScriptableObject
    {
        [Range(0.1f, 100f)] public float moveSpeed = 5f;
        [Range(45f, 270f)] public float rotateSpeedInDeg = 180f;
    }
}
