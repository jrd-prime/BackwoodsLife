using BackwoodsLife.Scripts.Framework.Helpers.Extensions;
using BackwoodsLife.Scripts.Gameplay.UI;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Manager.UIFrame
{
    public class MainFrameUI : UIView
    {
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.ToAbsolute();
        }

        public override void Show()
        {
            throw new System.NotImplementedException();
        }

        public override void Hide()
        {
            throw new System.NotImplementedException();
        }
    }
}
