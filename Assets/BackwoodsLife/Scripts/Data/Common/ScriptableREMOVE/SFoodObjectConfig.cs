using BackwoodsLife.Scripts.Data.Common.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BackwoodsLife.Scripts.Data.Common.ScriptableREMOVE
{
    [CreateAssetMenu(fileName = "name", menuName = "BLScriptable/Stored Objects/New FOOD config", order = 100)]
    public class SFoodObjectConfig : SCommonData
    {
        [Title("Food Stats")] [Range(0, 100)] public float foodStaminaRegen = 1;
        [Title("Food Effect")] public FoodEffect foodEffect = FoodEffect.None;

        [ShowIf("@foodEffect != FoodEffect.None")] [Range(1, 120)]
        public float foodEffectDuration = 1;

        [Title("Cookable")] public bool canBeCooked;

        [ShowIf("@canBeCooked == true")] [Range(1, 120)]
        public float cookingTime = 1;


        /* TODO Someday
        [Range(1, 100)] public float foodHPRegen = 1;
        [Range(0, 100)] public float foodStaminaRegen = 1;
        [Range(0, 100)] public float foodWaterRegen = 1; */
        /* TODO Someday
        [Title("Settings")] public bool canBeEatenRaw;
        [Title("Poisonable")] [Range(0.1f, 100.0f)]
        public float poisonChance = 0.1f;
        [Range(1f, 5f)] public float poisonDuration = 1;
        [Range(10f, 100f)] public float poisonDamage = 10;
         */
    }


    public enum FoodEffect
    {
        // TODO
        None = 0,
        Speed = 1,
    }
}
