using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Gameplay.NewLook;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Scriptable
{
    [CreateAssetMenu(fileName = "name_collect", menuName = "BLScriptable/Interactable Data/New Collectable Data",
        order = 100)]
    public class SCollectableData : ScriptableObject
    {
        public CollectRange collectRange;
   
    }
    
    
}
