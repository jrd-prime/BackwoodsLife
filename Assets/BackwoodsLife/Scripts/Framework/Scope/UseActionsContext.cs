using System.Collections.Generic;
using BackwoodsLife.Scripts.Framework.Item.UseAction;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BackwoodsLife.Scripts.Framework.Scope
{
    public class UseActionsContext : LifetimeScope
    {
        [SerializeField] private List<UseActionViewBase> useActionsView;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("Use actions context configure");
            // View
            foreach (var useActionView in useActionsView)
            {
                builder.RegisterComponent(useActionView);
            }

        }
    }
}
