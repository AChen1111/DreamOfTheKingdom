using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIButtonDoTweenScaler : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [Header("缩放目标（不填默认自己）")]
        [SerializeField] private Transform target;

        [Header("缩放参数")]
        [SerializeField] private float normalScale  = 1f;    // 默认大小
        [SerializeField] private float hoverScale   = 1.05f; // 移入大小
        [SerializeField] private float pressedScale = 1.15f; // 按下大小
        [SerializeField] private float duration     = 0.15f; // 动画时间

        private Tween _tween;

        private void Awake()
        {
            if (target == null)
                target = transform;

            target.localScale = Vector3.one * normalScale;
        }

        private void OnDisable()
        {
            KillTween();
            if (target != null)
                target.localScale = Vector3.one * normalScale;
        }

        private void KillTween()
        {
            if (_tween != null && _tween.IsActive())
                _tween.Kill();
            _tween = null;
        }

        private void PlayScale(float scale)
        {
            if (target == null) return;
            KillTween();
            _tween = target.DOScale(Vector3.one * scale, duration)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayScale(hoverScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PlayScale(normalScale);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PlayScale(pressedScale);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // 松开时，如果还在按钮上，就回到 hover，否则回到 normal
            bool stillOver = eventData.pointerCurrentRaycast.gameObject == gameObject;
            PlayScale(stillOver ? hoverScale : normalScale);
        }
    }
}