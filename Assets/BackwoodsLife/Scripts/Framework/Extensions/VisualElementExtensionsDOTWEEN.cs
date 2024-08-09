using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace BackwoodsLife.Scripts.Gameplay
{
    public static class VisualElementExtensionsDOTWEEN
    {
        public static void DoFadeIn(this VisualElement element, float duration)
        {
            // Начальная прозрачность 0
            element.style.opacity = 0f;
            element.style.display = DisplayStyle.Flex;

            // Анимация изменения прозрачности до 1
            DOTween.To(() => element.resolvedStyle.opacity, x => element.style.opacity = x, 1f, duration)
                .SetEase(Ease.InOutQuad);
        }

        public static void DoFadeOut(this VisualElement element, float duration)
        {
            // Анимация изменения прозрачности до 0
            DOTween.To(() => element.resolvedStyle.opacity, x => element.style.opacity = x, 0f, duration)
                .OnComplete(() => element.style.display = DisplayStyle.None);
        }

        public static void AnimateInFromBottom(this VisualElement element, int duration, float offsetY)
        {
            // Устанавливаем начальное положение ниже экрана на offsetY
            element.style.bottom = -offsetY;
            element.style.top = new StyleLength(StyleKeyword.Auto);
            element.style.display = DisplayStyle.Flex;

            // Анимация движения элемента вверх до его оригинальной позиции
            element.experimental.animation.Start(
                    new StyleValues { bottom = 0 },
                    duration)
                .Ease(Easing.OutBounce);
        }

        public static void DoMoveOutToBottom(this VisualElement element, float duration, float offsetY,
            Ease easeType = Ease.Linear)
        {
            // Анимация движения элемента вниз на offsetY с эффектом Ease
            DOTween.To(() => element.resolvedStyle.bottom, x => element.style.bottom = x, -offsetY, duration)
                .SetEase(easeType)
                .OnComplete(() => element.style.display = DisplayStyle.None);
        }
    }
}
