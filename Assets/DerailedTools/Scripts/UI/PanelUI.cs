using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DerailedTools.UI
{
    public abstract class PanelUI<T> : MonoBehaviour
    {
        [Header("Default Settings")]
        [SerializeField] protected Transform panel;
        [SerializeField] protected float appearTime, disappearTime;
        [SerializeField] protected Ease openingAnimation, closingAnimation;
        [SerializeField] protected UnityEvent panelClosed;

        protected Vector3 defaultScale;
        protected bool isClosing;

        protected virtual void Start()
        {
            defaultScale = panel.localScale;
        }

        public virtual void ShowPanel(T data)
        {
            if (panel.gameObject.activeSelf) return;

            isClosing = false;
            panel.DOKill();
            panel.DOScale(0.001f, 0);
            panel.gameObject.SetActive(true);
            panel.DOScale(defaultScale, appearTime).SetEase(openingAnimation).SetUpdate(true);
        }

        public virtual async void ClosePanel()
        {
            if (isClosing) return;

            isClosing = true;
            panel.DOKill();
            await panel.DOScale(0.001f, disappearTime).SetEase(closingAnimation).SetUpdate(true).AsyncWaitForCompletion();
            panel.gameObject.SetActive(false);
            panel.DOScale(defaultScale, 0);
            panelClosed?.Invoke();
        }
    }
}