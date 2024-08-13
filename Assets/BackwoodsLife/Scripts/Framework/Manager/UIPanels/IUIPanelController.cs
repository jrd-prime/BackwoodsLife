using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIPanels
{
    public interface IUIPanelController 
    {
        public VisualTreeAsset GetTemplateFor(string inWindow);
    }
}
