using BackwoodsLife.Scripts.Data.Const.UI;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Extensions.Helpers
{
    public static class UIHelper
    {
        public static void SetDefaultCanvasSize(ref VisualElement root)
        {
            Assert.IsNotNull(root, $"The {nameof(root)} parameter cannot be null.");
            root.style.height = UIConst.RootVisualElementCanvasHeight;
            root.style.width = UIConst.RootVisualElementCanvasWidth;
        }
    }
}