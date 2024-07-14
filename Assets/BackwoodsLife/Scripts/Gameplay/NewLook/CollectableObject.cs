using BackwoodsLife.Scripts.Framework.Systems;
using BackwoodsLife.Scripts.Gameplay.Environment.Interactable;
using UnityEngine;

namespace BackwoodsLife.Scripts.Gameplay.NewLook
{
    public class CollectableObject : CustomInteractableObject<SCollectable>
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
