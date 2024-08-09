using BackwoodsLife.Scripts.Data.Const;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Scriptable.Items.WorldItem
{
    /// <summary>
    /// То что мы можем как-либо использовать. Например, колодец
    /// </summary>
    [CreateAssetMenu(
        fileName = "UseOnlyItem",
        menuName = SOPathName.WorldItemPath + "Use Only Item",
        order = 1)]
    public class SUseOnlyItem : SWorldItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            interactTypes = EInteractTypes.Use;
        }
    }
}
