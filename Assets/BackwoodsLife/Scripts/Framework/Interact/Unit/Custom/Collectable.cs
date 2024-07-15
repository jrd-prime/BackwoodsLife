using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Framework.Interact.System;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class Collectable : CustomInteractableObject<SCollectable>
    {
        private CollectSystem _collectSystem;

        public override void Process(IInteractableSystem interactableSystem)
        {
            Assert.IsNotNull(interactableSystem, "interactableSystem is null");
            _collectSystem = interactableSystem as CollectSystem;
            Assert.IsNotNull(_collectSystem, "interactableSystem is not CollectSystem");

            if (localData.isReturnCollectables)
            {
                Debug.LogWarning("HAS COLLECTABLE");
                if (localData.hasRequirements)
                {
                    Debug.LogWarning("HAS REQUIREMENTS");
                }
                else
                {
                    Debug.LogWarning("NO REQUIREMENTS just collect");
                    


                     _collectSystem.Collect(ref localData.returnableElements);
                } 
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
