using BackwoodsLife.Scripts.Data.Common.Enums;
using UnityEngine.AddressableAssets;

namespace BackwoodsLife.Scripts.Data.Common.Scriptable.Items.WorldItem
{
    public class SWorldItemsConfig : SItemConfig
    {
        public EWorldItem worldItemType;

        public AssetReferenceGameObject mainPrefab;
    }
}
