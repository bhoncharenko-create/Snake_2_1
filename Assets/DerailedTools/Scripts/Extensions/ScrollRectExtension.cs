using DG.Tweening;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace DerailedTools.UI
{
    public static class ScrollRectExtension
    {
        public static void ScrollToObjectY(this ScrollRect scrollRect, RectTransform target, float duration = 0, bool lockScroll = true, float offsetPercentage = 0)
        {
            Canvas.ForceUpdateCanvases();
            float scrollOffsetY = scrollRect.viewport.rect.height * (offsetPercentage / 100);

            Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
            Vector2 targetLocalPosition = target.localPosition;

            Vector2 newTargetLocalPosition = new Vector2(
                scrollRect.content.localPosition.x,
                -(viewportLocalPosition.y + targetLocalPosition.y) +
                (scrollRect.viewport.rect.height / 2) -
                (target.rect.height / 2) + scrollOffsetY
            );

            if (lockScroll)
                scrollRect.enabled = false;

            scrollRect.content
                .DOLocalMove(newTargetLocalPosition, duration)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => { scrollRect.enabled = true; });
        }
    }
}