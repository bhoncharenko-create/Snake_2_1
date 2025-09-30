using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DerailedTools.UI
{
    public class PageButtonsManager : MonoBehaviour
    {
        public static event Action<GameObject> PageChanged;

        [SerializeField] private PageButton[] buttons;
        [SerializeField] private Color selectedColor;
        [SerializeField] private int startPageIndex;

        private Vector3 defaultButtonScale;
        private Color defaultButtonColor;

        private static PageButton currentSelectedButton;

        private void OnEnable()
        {
            defaultButtonScale = buttons[startPageIndex].Button.transform.localScale;
            defaultButtonColor = buttons[startPageIndex].Button.image.color;

            foreach (var button in buttons)
            {
                button.Button.onClick.AddListener(() => OpenPage(button));
            }

            buttons[startPageIndex].Button.onClick?.Invoke();
        }

        private void OnDisable()
        {
            currentSelectedButton = null;
        }

        public void OpenPage(PageButton pageButton)
        {
            if (currentSelectedButton != null && currentSelectedButton.TargetPage == pageButton.TargetPage)
                return;

            foreach (var button in buttons)
            {
                if (button.TargetPage == pageButton.TargetPage)
                    button.TargetPage.SetActive(true);
                else
                    button.TargetPage.SetActive(false);
            }

            PageChanged?.Invoke(pageButton.TargetPage);
            StopAllCoroutines();
            StartCoroutine(ButtonAnimation(pageButton));
        }

        private IEnumerator ButtonAnimation(PageButton pageButton)
        {
            if (currentSelectedButton != null)
            {
                var button = currentSelectedButton.Button;
                button.image.color = defaultButtonColor;
                button.transform.DOKill();
                button.transform.localScale = defaultButtonScale;
            }

            currentSelectedButton = pageButton;
            pageButton.Button.transform.SetAsLastSibling();
            yield return pageButton.Button.transform.DOScale(defaultButtonScale * 0.8f, 0.05f).SetEase(Ease.Flash).WaitForCompletion();
            pageButton.Button.image.color = selectedColor;
            yield return pageButton.Button.transform.DOScale(defaultButtonScale * 1.2f, 0.1f).SetEase(Ease.Flash).WaitForCompletion();
        }

        [Serializable]
        public class PageButton
        {
            [field: SerializeField] public GameObject TargetPage;
            [field: SerializeField] public Button Button;
        }
    }
}