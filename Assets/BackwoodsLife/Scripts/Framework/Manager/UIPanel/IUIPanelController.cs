using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanel
{
    public interface IUIPanelController
    {
        public VisualTreeAsset GetTemplateFor(string inWindow);
    }
}
