using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Inventory;
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
                    var list = new List<InventoryElement>();

                    foreach (var returnedCollectable in localData.returnedCollectables)
                    {
                        Debug.LogWarning(returnedCollectable);
                    }


                    // _collectSystem.Collect(ref list);
                }
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
