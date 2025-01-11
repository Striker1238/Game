using DG.Tweening;
using UnityEngine;
//TODO: уменьшение статов только когда идет прокачка и выбор куда влить поинт
namespace UI
{
    public class UIAnimationHandler : MonoBehaviour
    {
        private Sequence currentAnimation;
        private float speedAnimation = 0.7f;
        private Vector3 offsetPosition;

        public void Initialize(GameObject inventoryPanel, GameObject statsBookPanel)
        {
            inventoryPanel.SetActive(false);
            statsBookPanel.SetActive(false);
        }

        public void TogglePanel(GameObject panel)
        {
            bool isActive = panel.activeSelf;

            if (currentAnimation != null) currentAnimation.Complete();

            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            Vector3 startPos = rectTransform.anchoredPosition;
            Vector3 endPos = isActive ? startPos - offsetPosition : startPos + offsetPosition;

            currentAnimation = DOTween.Sequence()
                .OnStart(() => { if (!isActive) panel.SetActive(true); })
                .Append(canvasGroup.DOFade(isActive ? 0 : 1, speedAnimation))
                .Join(rectTransform.DOAnchorPos(isActive ? startPos : endPos, speedAnimation))
                .OnComplete(() =>
                {
                    if (canvasGroup.alpha == 0)
                        panel.SetActive(false);
                });
        }
    }
}
