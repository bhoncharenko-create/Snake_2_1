using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    [Header("=== НАСТРОЙКИ ЗАГРУЗКИ ===")]
    [SerializeField] private float loadingTime = 3f;

    [Header("=== UI ЭЛЕМЕНТЫ - ПЕРЕТАЩИТЕ СЮДА ===")]
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI percentText;
    [SerializeField] private GameObject startButtonPanel;
    [SerializeField] private Button startButton;

    private AsyncOperation asyncLoad;

    private void Start()
    {
        SetupUI();
        StartCoroutine(LoadGameScene());
    }
    private void SetupUI()
    {
        // Скрываем панель с кнопкой в начале
        startButtonPanel.SetActive(false);

        // Настраиваем кнопку
        startButton.onClick.AddListener(StartGame);

        // Устанавливаем начальные значения
        percentText.text = "0%";
        progressSlider.value = 0f;
    }

    private IEnumerator LoadGameScene()
    {
        // Ждем один кадр
        yield return null;

        // Начинаем загрузку сцены
        asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        asyncLoad.allowSceneActivation = false;

        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingTime);
            int currentPercent = Mathf.RoundToInt(progress * 100);

            // Обновляем прогресс
            UpdateProgressUI(currentPercent, progress);

            yield return null;
        }

        // Загрузка завершена
        UpdateProgressUI(100, 1f);
        ShowStartButton();
    }

    private void UpdateProgressUI(int percent, float progress)
    {
        percentText.text = $"{percent}%";
        progressSlider.value = progress;
    }

    private void ShowStartButton()
    {
        // Показываем кнопку старта
        startButtonPanel.SetActive(true);
    }

    private void StartGame()
    {
        // Переходим к игре
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}