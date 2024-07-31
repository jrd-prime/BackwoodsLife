using UnityEngine.UIElements;

namespace BackwoodsLife.Scripts.Framework.Helpers.Extensions
{
    public static class VisualElementExtension
    {
        public static void ToAbsolute(this VisualElement element)
        {
            element.style.position = new StyleEnum<Position>(Position.Absolute);
            element.style.left = new StyleLength(0f);
            element.style.right = new StyleLength(0f);
            element.style.top = new StyleLength(0f);
            element.style.bottom = new StyleLength(0f);
        }
    }
}
