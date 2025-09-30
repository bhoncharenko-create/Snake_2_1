using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    [Header("=== ��������� �������� ===")]
    [SerializeField] private float loadingTime = 3f;

    [Header("=== UI �������� - ���������� ���� ===")]
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
        // �������� ������ � ������� � ������
        startButtonPanel.SetActive(false);

        // ����������� ������
        startButton.onClick.AddListener(StartGame);

        // ������������� ��������� ��������
        percentText.text = "0%";
        progressSlider.value = 0f;
    }

    private IEnumerator LoadGameScene()
    {
        // ���� ���� ����
        yield return null;

        // �������� �������� �����
        asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        asyncLoad.allowSceneActivation = false;

        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingTime);
            int currentPercent = Mathf.RoundToInt(progress * 100);

            // ��������� ��������
            UpdateProgressUI(currentPercent, progress);

            yield return null;
        }

        // �������� ���������
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
        // ���������� ������ ������
        startButtonPanel.SetActive(true);
    }

    private void StartGame()
    {
        // ��������� � ����
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}