using System;
using System.Collections.Generic;

namespace BackwoodsLife.Scripts.Framework.Manager.Quest
{
    public sealed class PanelUIController
    {
        private readonly Dictionary<Type, IUIPanelController> _controllersCache = new();

        public PanelUIController(List<IUIPanelController> controllers)
        {
            foreach (var uiPanelController in controllers)
            {
                _controllersCache.TryAdd(uiPanelController.GetType(), uiPanelController);
            }
        }

        public T GetController<T>() where T : class, IUIPanelController
        {
            if (!_controllersCache.ContainsKey(typeof(T)))
                throw new KeyNotFoundException($"Controller {typeof(T)} not found");

            return _controllersCache[typeof(T)] as T;
        }
    }
}
