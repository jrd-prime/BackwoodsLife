using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Scriptable.Interactable;
using BackwoodsLife.Scripts.Data.Inventory;
using BackwoodsLife.Scripts.Framework.Helpers;
using BackwoodsLife.Scripts.Framework.Interact.System;
using UnityEngine;
using UnityEngine.Assertions;

namespace BackwoodsLife.Scripts.Framework.Interact.Unit.Custom
{
    public class Collectable : CustomInteractableObject<SInWorldInWorldCollectable>
    {
        private CollectSystem _collectSystem;

        public override void Process(IInteractableSystem interactableSystem, Action<List<InventoryElement>> callback)
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
                    var list = localData.returnElements.Select(element => new InventoryElement
                        {
                            typeName = element.Name,
                            Amount = RandomCollector.GetRandom(element.Range.min, element.Range.max)
                        })
                        .ToList();

                    callback.Invoke(list);

                    _collectSystem.Collect(ref list);
                }
            }
            else
            {
                Debug.LogWarning("NO COLLECTABLE");
            }
        }
    }
}
