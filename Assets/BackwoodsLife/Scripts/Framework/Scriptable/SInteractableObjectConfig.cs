using BackwoodsLife.Scripts.Framework.Scriptable.Interactable;
using BackwoodsLife.Scripts.Framework.Scriptable.obj;
using BackwoodsLife.Scripts.Gameplay.Environment;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Types;
using Sirenix.OdinInspector;
using UnityEngine;

//TODO add checks
namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name_config", menuName = "BLScriptable/Interactable Data/Main object config",
        order = 100)]
    public class SInteractableObjectConfig : ScriptableObject
    {
        public EInteractableObjectType interactableType = EInteractableObjectType.NotSet;

        [ShowIf("@interactableType == EInteractableObjectType.Collectable")]
        public PlayerObjectType objectType = PlayerObjectType.None;

        [ShowIf("@objectType == PlayerObjectType.Tool")]
        public EToolType toolType = EToolType.None;

        [ShowIf("@objectType == PlayerObjectType.Resource")]
        public EResourceType resourceType = EResourceType.None;

        [ShowIf("@objectType == PlayerObjectType.Food")]
        public EFoodType foodType = EFoodType.None;

        [ShowIf("@interactableType == EInteractableObjectType.Collectable")]
        public SCollectableData collectableData;

        #region HasUpgradeLevels

        [Title("Levels")] public bool hasLevels;
        [ShowIf("@hasLevels == false")] public SLevelData defaultLevelData;

        [ShowIf("@hasLevels == true")] public SLevelData[] levelsData;

        #endregion

        #region InStorageData

        [Title("InStorageData")] [ShowIf("@interactableType == EInteractableObjectType.Collectable")]
        public SStorageData inStorageData;

        #endregion

        #region FixedWorldPosition

        [Title("Fixed World Position")] public bool fixedWorldPosition;

        [ShowIf("@fixedWorldPosition == true")]
        public Vector3 fixedPosition;

        #endregion
    }
}
