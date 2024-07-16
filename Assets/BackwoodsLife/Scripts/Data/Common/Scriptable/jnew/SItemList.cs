using System.Collections.Generic;
using UnityEngine;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.jnew
{
    [CreateAssetMenu(fileName = "ItemsList",
        menuName = "Backwoods Life Scripts/Items list confog", order = 1)]
    public class SItemList : ScriptableObject
    {
        public List<SItem> items;
    }
}
