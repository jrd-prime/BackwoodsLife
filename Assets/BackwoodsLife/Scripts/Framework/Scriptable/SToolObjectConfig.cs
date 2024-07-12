using BackwoodsLife.Scripts.Data.Inventory.JObjects.ToolObjects;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name", menuName = "BLScriptable/Stored Objects/New TOOL config", order = 100)]
    public class SToolObjectConfig : SCommonData
    {
        public EToolType toolType = EToolType.None;
    }
}
