using BackwoodsLife.Scripts.Data.Common.Scriptable.Items;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.UsableAndUpgradable
{
    public class Bonfire : WorldItemNew
    {
    }

    public abstract class WorldItemNew : MonoBehaviour
    {
        [SerializeField] protected SWorldItemConfig worldItemConfig;
    }
}
