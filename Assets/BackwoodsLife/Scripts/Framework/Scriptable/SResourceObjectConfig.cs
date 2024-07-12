using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name", menuName = "BLScriptable/Stored Objects/New RESOURCE config", order = 100)]
    public class SResourceObjectConfig : SCommonData
    {
        public EResourceType resourceType = EResourceType.None;
    }
}
