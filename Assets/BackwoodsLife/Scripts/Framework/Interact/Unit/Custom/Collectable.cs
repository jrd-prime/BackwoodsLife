using BackwoodsLife.Scripts.Data.Common.Scriptables;
using BackwoodsLife.Scripts.Framework.Interact.System;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class Collectable : CustomInteractableObject<SCollectable>
    {
        public override void Process(IInteractableSystem interactableSystem)
        {
            if (localData.hasCollectable)
            {
                Debug.LogWarning("HAS COLLECTABLE");
                if (localData.hasRequirements)
                {
                    Debug.LogWarning("HAS REQUIREMENTS");
                }
                else
                {
                    Debug.LogWarning("NO REQUIREMENTS just collect");
                }
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
